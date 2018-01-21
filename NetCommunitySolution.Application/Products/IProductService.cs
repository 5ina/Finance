using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Products;
using System.Collections.Generic;

namespace NetCommunitySolution.Products
{
    public interface IProductService : IApplicationService
    {
        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="product"></param>
        int InsertProduct(Product product);

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="product"></param>
        void UpdateProduct(Product product);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="productId"></param>
        void DeleteProduct(int productId);

        /// <summary>
        /// 根据商品主键获取商品信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product GetProductById(int productId);

        /// <summary>
        /// 获取所有的商品
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="categoryIds"></param>
        /// <param name="brand"></param>
        /// <param name="priceMin"></param>
        /// <param name="priceMax"></param>
        /// <param name="showHidden"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<Product> GetAllProducts(string keywords = null,
            int categoryId = 0,
            bool showHidden = false,
            bool? allowReward = null,
            bool? special = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);
    }
}
