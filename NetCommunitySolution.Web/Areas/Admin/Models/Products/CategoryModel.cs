using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Products;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Products
{
    [AutoMap(typeof(Category))]
    public class CategoryModel : EntityDto
    {
        public CategoryModel()
        {
            this.AvailableParentCategories = new List<SelectListItem>();
        }
        [DisplayName("类别名称")]
        public string Name { get; set; }              

        [DisplayName("父类")]
        public int ParentId { get; set; }
        [DisplayName("是否发布")]
        public bool Published { get; set; }
        [DisplayName("权重")]
        public int DisplayOrder { get; set; }

        public IList<SelectListItem> AvailableParentCategories { get; set; }
    }
}