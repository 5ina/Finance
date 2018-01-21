using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Products;

namespace NetCommunitySolution.Products
{
    public class ProductImagesService : NetCommunitySolutionAppServiceBase, IProductImagesService
    {


        #region Ctor && Field

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product Id
        /// </remarks>
        private const string PRODUCTIMAGE_DEFAULT_PRODUCT_KEY = "net.productiamge.default-{0}";



        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product Id
        /// </remarks>
        private const string PRODUCTIMAGE_PRODUCT_ID = "net.productiamge.product-{0}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTIMAGE_PATTERN_KEY = "net.productiamge.";

        private readonly IRepository<ProductImage> _imageRepository;
        private readonly ICacheManager _cacheManager;

        public ProductImagesService(IRepository<ProductImage> imageRepository,
            ICacheManager cacheManager)
        {
            this._imageRepository = imageRepository;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Method
        public void ClearImages(int productId)
        {
            _imageRepository.Delete(i => i.ProductId == productId);

            _cacheManager.GetCache(string.Format(PRODUCTIMAGE_PRODUCT_ID))
                .Remove(string.Format(PRODUCTIMAGE_PRODUCT_ID));

            _cacheManager.GetCache(string.Format(PRODUCTIMAGE_DEFAULT_PRODUCT_KEY))
                .Remove(string.Format(PRODUCTIMAGE_DEFAULT_PRODUCT_KEY));
        }

        public void DeleteImage(int imageId)
        {
            _imageRepository.Delete(imageId);
        }

        
        public ProductImage GetProductImageById(int imageId)
        {
            if (imageId > 0)
                return _imageRepository.Get(imageId);
            return null;
        }

        public IList<ProductImage> GetProductImagesByProductId(int productId)
        {
            var key = string.Format(PRODUCTIMAGE_PRODUCT_ID, productId);
            return _cacheManager.GetCache(key).Get(key, () => {
                return _imageRepository.GetAllList(i => i.ProductId == productId);
            });

        }

        public int InsertImage(ProductImage image)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            var imageId = _imageRepository.InsertAndGetId(image);

            //cache
            _cacheManager.RemoveByPattern(PRODUCTIMAGE_PATTERN_KEY);

            return imageId;
        }

        public void UpdateImage(ProductImage image)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            if (image.DefaultImage)
            {
                //全部至于非选项
                var imageList = _imageRepository.GetAllList(i => i.ProductId == image.ProductId && i.DefaultImage);

                imageList.Select(i =>
                {
                    var defaultImage = i;
                    defaultImage.DefaultImage = false;
                    _imageRepository.Update(defaultImage);
                    return defaultImage;
                });
            }

            _imageRepository.Update(image);

            //cache
            _cacheManager.RemoveByPattern(PRODUCTIMAGE_PATTERN_KEY);
        }

        public ProductImage GetProductDefaultImage(int productId)
        {
            var key = string.Format(PRODUCTIMAGE_DEFAULT_PRODUCT_KEY, productId);
            return _cacheManager.GetCache(key).Get(key, () => {
                return _imageRepository.FirstOrDefault(i => i.ProductId == productId && i.DefaultImage);
            });
        }
        
        #endregion
    }
}
