using Abp.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.Html
{

    /// <summary>
    /// Select list helper
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Get category list
        /// </summary>
        /// <param name="categoryService">Category service</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category list</returns>
        public static List<SelectListItem> GetCategoryList(ICategoryService categoryService, ICacheManager cacheManager, bool showHidden = false)
        {
                var categories = categoryService.GetAllCategories(showHidden: showHidden);
                var categoryListItems = categories.Items.Select(c => new SelectListItem
                {
                    Text = c.GetFormattedBreadCrumb(categories.Items.ToList()),
                    Value = c.Id.ToString()
                }).ToList();

            var result = new List<SelectListItem>();
            foreach (var item in categoryListItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }
    }
}