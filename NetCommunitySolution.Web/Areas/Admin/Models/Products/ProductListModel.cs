using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Products
{
    public class ProductListModel
    {

        public ProductListModel()
        {
            this.AvailableCategories = new List<SelectListItem>();
        }

        [DisplayName("关键字")]
        public string Keywords { get; set; }
        
        public int CategoryId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        [DisplayName("是否预售")]
        public bool? AllowReward { get; set; }

    }
}