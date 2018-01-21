using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Products;
using System.Collections.Generic;

namespace NetCommunitySolution.Products
{
    /// <summary>
    /// 类目服务接口
    /// </summary>
    public interface ICategoryService : IApplicationService
    {
        /// <summary>
        /// 查询所有类目
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="parentId">父Id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showHidden"></param>
        /// <returns></returns>
        IPagedResult<Category> GetAllCategories(string keywords = "", int? parentId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// 查询树状类别
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <param name="showHidden"></param>
        /// <param name="includeAllLevels"></param>
        /// <returns></returns>
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false, bool includeAllLevels = false);

        /// <summary>
        /// 查询首页显示的类目
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns></returns>
        IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false);

        /// <summary>
        /// 根据Id获取类目
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// 新增类目
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        int InsertCategory(Category category);

        /// <summary>
        /// 更新类目
        /// </summary>
        /// <param name="category"></param>
        void UpdateCategory(Category category);

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="categoryId"></param>
        void DeleteCategory(int categoryId);

    }
}
