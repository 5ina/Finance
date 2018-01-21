using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using NetCommunitySolution.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCommunitySolution.Products
{
    public class ProductService : NetCommunitySolutionAppServiceBase, IProductService
    {
        #region Ctor && Field

        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManage;
        public ProductService(IRepository<Product> productRepository,
            IUnitOfWorkManager unitOfWorkManage)
        {
            this._productRepository = productRepository;
            this._unitOfWorkManage = unitOfWorkManage;
        }
        #endregion

        #region Method


        public void DeleteProduct(int productId)
        {
            if (productId > 0)
                _productRepository.Delete(productId);
        }
        

        public IPagedResult<Product> GetAllProducts(string keywords = null, 
            int categoryId = 0,
            bool showHidden = false, bool? allowReward = null,
            bool? special = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            using (_unitOfWorkManage.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _productRepository.GetAll();

                if (!String.IsNullOrWhiteSpace(keywords))
                    query = query.Where(p => p.Name.Contains(keywords));
                
                if (!showHidden)
                    query = query.Where(p => !p.IsDeleted);

                if (categoryId > 0)
                    query = query.Where(p => p.CategoryId == categoryId);

                if (allowReward.HasValue)
                    query = query.Where(p => p.AllowReward == allowReward.Value);

                if (special.HasValue && special.Value)
                    query = query.Where(p => p.SpecialPrice.HasValue &&
                     p.SpecialPriceEndDateTime.Value.Date > DateTime.Now);

                if (special.HasValue && !special.Value)
                    query = query.Where(p => p.SpecialPriceStartDateTime.Value.Date < DateTime.Now &&
                     p.SpecialPriceEndDateTime.Value.Date > DateTime.Now);


                query = query.OrderByDescending(p => p.DisplayOrder);
                return new PagedResult<Product>(query, pageIndex, pageSize);
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                using (_unitOfWorkManage.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    return _productRepository.Get(productId);
                }
            }
            catch
            {
                return null;
            }
        }

        public int InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            return _productRepository.InsertAndGetId(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            using (_unitOfWorkManage.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                _productRepository.Update(product);
            }
        }


        #endregion
    }
}
