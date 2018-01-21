using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Products
{
    [AutoMap(typeof(Product))]
    public class ProductModel : EntityDto
    {
        public ProductModel()
        {
            this.AvailableCategories = new List<SelectListItem>();
            this.AvailablePictures = new List<ProductPictureModel>();
            this.AddPictureModel = new ProductPictureModel();
        }
        [DisplayName("商品类别")]
        public int CategoryId { get; set; }
        [DisplayName("商品编号")]
        public string ProductCode { get; set; }

        [DisplayName("商品名称")]
        public string Name { get; set; }
        [DisplayName("介绍")]
        public string ShortDescription { get; set; }

        [DisplayName("图文介绍")]
        [UIHint("Editor")]
        public string FullDescription { get; set; }
        

        [DisplayName("库存")]
        public int StockQuantity { get; set; }

        [DisplayName("销售价")]
        public decimal Price { get; set; }
        [DisplayName("市场价")]
        public decimal Market { get; set; }
        [DisplayName("成本")]
        public decimal Cost { get; set; }
        
        [DisplayName("积分兑换")]
        public bool AllowReward { get; set; }
        /// <summary>
        /// 特殊价格
        /// </summary>
        [DisplayName("促销价")]
        public decimal? SpecialPrice { get; set; }

        [DisplayName("促销日期（开始）")]
        [UIHint("DateNullable")]
        public DateTime? SpecialPriceStartDateTime { get; set; }

        [DisplayName("促销日期（结束）")]
        [UIHint("DateNullable")]
        public DateTime? SpecialPriceEndDateTime { get; set; }


        [DisplayName("权重")]
        public int DisplayOrder { get; set; }
        [DisplayName("是否上架")]
        public bool Published { get; set; }
                
        public ProductPictureModel AddPictureModel { get; set; }
        public IList<ProductPictureModel> AvailablePictures { get; set; }


        public IList<SelectListItem> AvailableCategories { get; set; }


        [AutoMap(typeof(ProductImage))]
        public partial class ProductPictureModel
        {
            [DisplayName("图片")]
            public int Id { get; set; }

            public int ProductId { get; set; }

            public bool DefaultImage { get; set; }

            [UIHint("PictureUrl")]
            [DisplayName("图片地址")]
            public string PictureUrl { get; set; }
        }        

    }
}