using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCommunitySolution.Products
{
    public class CategoryService : NetCommunitySolutionAppServiceBase, ICategoryService
    {

        #region Fields
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// </remarks>
        private const string CATEGORIES_BY_ID_KEY = "net.category.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent category ID
        /// {1} : show hidden records?
        /// {2} : include all levels (child)
        /// </remarks>
        private const string CATEGORIES_BY_PARENT_CATEGORY_ID_KEY = "net.category.byparent-{0}-{1}-{2}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CATEGORIES_PATTERN_KEY = "net.category.";

        #endregion

        #region Ctor


        public CategoryService(IRepository<Category> categoryRepository, ICacheManager cacheManager)
        {
            this._categoryRepository = categoryRepository;
            this._cacheManager = cacheManager;
        }
        #endregion


        #region Method
        public void DeleteCategory(int categoryId)
        {
            var subcategories = GetAllCategoriesByParentCategoryId(categoryId, true);
            foreach (var subcategory in subcategories)
            {
                _categoryRepository.Delete(subcategory.Id);
            }
            _categoryRepository.Delete(categoryId);
        }

        public IPagedResult<Category> GetAllCategories(string keywords = "", int? parentId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {


            var query = _categoryRepository.GetAll();
            if (!showHidden)
                query = query.Where(c => c.Published);

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(c => c.Name.Contains(keywords));

            query = query.OrderBy(c => c.ParentId).ThenBy(c => c.DisplayOrder);


            var unsortedCategories = query.ToList();

            //sort categories
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedResult<Category>(sortedCategories, pageIndex, pageSize);
        }

        public IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId, bool showHidden = false, bool includeAllLevels = false)
        {
            string key = string.Format(CATEGORIES_BY_PARENT_CATEGORY_ID_KEY, parentCategoryId, showHidden,
                 includeAllLevels);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var query = _categoryRepository.GetAll();
                if (!showHidden)
                    query = query.Where(c => c.Published);

                query = query.Where(c => c.ParentId == parentCategoryId);

                query = query.OrderBy(c => c.DisplayOrder);

                var categories = query.ToList();
                if (includeAllLevels)
                {
                    var childCategories = new List<Category>();

                    foreach (var category in categories)
                    {
                        childCategories.AddRange(GetAllCategoriesByParentCategoryId(category.Id, showHidden, includeAllLevels));
                    }
                    categories.AddRange(childCategories);
                }
                return categories;
            });
        }

        public IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false)
        {
            var query = from c in _categoryRepository.GetAll()
                        orderby c.DisplayOrder
                        where c.Published 
                        orderby c.ParentId, c.DisplayOrder descending
                        select c;

            var categories = query.ToList();
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;

            string key = string.Format(CATEGORIES_BY_ID_KEY, categoryId);
            return _cacheManager.GetCache(key).Get(key, () => _categoryRepository.Get(categoryId));
        }

        public int InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var categoryId = _categoryRepository.InsertAndGetId(category);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);

            return categoryId;
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            //validate category hierarchy
            var parentCategory = GetCategoryById(category.ParentId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentId = 0;
                    break;
                }
                parentCategory = GetCategoryById(parentCategory.ParentId);
            }

            _categoryRepository.Update(category);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);

        }

        #endregion
    }
}
