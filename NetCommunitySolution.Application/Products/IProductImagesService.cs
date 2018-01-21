using Abp.Application.Services;
using NetCommunitySolution.Domain.Products;
using System.Collections.Generic;

namespace NetCommunitySolution.Products
{
    /// <summary>
    /// 商品图片信息
    /// </summary>
    public interface IProductImagesService : IApplicationService
    {
        /// <summary>
        /// 新增图片
        /// </summary>
        /// <param name="image"></param>
        int InsertImage(ProductImage image);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="image"></param>
        void UpdateImage(ProductImage image);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="imageId"></param>
        void DeleteImage(int imageId);

        /// <summary>
        /// 清空图片
        /// </summary>
        /// <param name="productId"></param>
        void ClearImages(int productId);

        /// <summary>
        /// 获取商品默认图片
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductImage GetProductDefaultImage(int productId);

        /// <summary>
        /// 根据主键获取图片
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        ProductImage GetProductImageById(int imageId);

        /// <summary>
        /// 获取商品的图片集
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IList<ProductImage> GetProductImagesByProductId(int productId);
    }
}
