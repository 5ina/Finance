using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Abp.UI;
using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Common;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Domain.Products;
using NetCommunitySolution.Media;
using NetCommunitySolution.Products;
using NetCommunitySolution.Web.Areas.Admin.Models.Products;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region ctor && Fields

        private const string ATTRIBUTECACHE = "stroe.attribute.all";

        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductImagesService _productImageService;
        private readonly IOssService _imageService;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;


        public ProductController(ICategoryService categoryService,
            IProductService productService,
            IProductImagesService productImageService,
            ICacheManager cacheManager,
            IOssService imageService,
            ISettingService settingService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
            this._productImageService = productImageService;
            this._cacheManager = cacheManager;
            this._imageService = imageService;
            this._settingService = settingService;
        }
        #endregion


        #region Utilities
        [NonAction]
        protected void PrepareProductListModel(ProductListModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");

            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories.Items)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories.Items.ToList()),
                    Value = c.Id.ToString()
                });
            }
            model.AvailableCategories.Insert(0, new SelectListItem
            {
                Text = "请选择类别",
                Selected = true,
                Value = "0"
            });
        }

        [NonAction]
        protected void PrepareProductModel(ProductModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");

            model.AvailableCategories.Add(new SelectListItem
            {
                Text = "[请选择类别]",
                Value = "0"
            });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories.Items)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories.Items.ToList()),
                    Value = c.Id.ToString()
                });
            }
        }

        [NonAction]
        protected void PrepareProductImagesModel(ProductModel model)
        {

            if (model == null)
                throw new UserFriendlyException("model");
            var images = _productImageService.GetProductImagesByProductId(model.Id);

            foreach (var item in images)
            {
                model.AvailablePictures.Add(new ProductModel.ProductPictureModel
                {
                    Id = item.Id,
                    PictureUrl = item.Url,
                    ProductId = item.ProductId
                });
            }
        }

        #endregion

        #region Index
        public ActionResult Index()
        {
            var model = new ProductListModel();
            PrepareProductListModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, ProductListModel model)
        {
            var products = _productService.GetAllProducts(keywords: model.Keywords,
                                                        categoryId: model.CategoryId,
                                                        showHidden: true,
                                                        allowReward :model.AllowReward,
                                                        pageIndex: command.page - 1,
                                                        pageSize: command.limit);
            var jsonData = new DataSourceResult
            {
                data = products.Items,
                count = products.TotalCount
            };
            return AbpJson(jsonData);
        }

        public ActionResult Create()
        {
            var model = new ProductModel();
            PrepareProductModel(model);
            model.DisplayOrder = 999;
            model.Published = true;
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ProductModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Product>();
                model.Id = _productService.InsertProduct(entity);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("Index");
            }
            PrepareProductModel(model);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return RedirectToAction("List");

            var model = product.MapTo<ProductModel>();
            PrepareProductModel(model);
            PrepareProductImagesModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadImage(int productId )
        {
            if (Request.Files.Count <= 0)
                return AbpJson(null);


            HttpPostedFileBase httpPostedFile = Request.Files[0];
            Stream stream = httpPostedFile.InputStream;
            var fileName = Path.GetFileName(httpPostedFile.FileName);
            var contentType = httpPostedFile.ContentType;

            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            var mediaMode = _settingService.GetSettingByKey<MediaMode>(MediaSettingNames.MediaMode);
            var url = string.Empty;

            if (mediaMode == MediaMode.Alyun)
                url = _imageService.UploadImage(images: fileBinary, isBuildThumbnail: true);

            var imageId = _productImageService.InsertImage(new ProductImage {
                ProductId = productId,
                DefaultImage =false,
                Url = url,
            });

            return AbpJson(new
            {
                code = 0,
                msg = "",
                data = new
                {
                    src = url,
                    Id = imageId,
                }
            });
        }

        #endregion

    }
}