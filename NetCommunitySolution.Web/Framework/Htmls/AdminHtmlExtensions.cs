using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NetCommunitySolution.Web.Framework.Htmls
{
    public static class AdminHtmlExtensions
    {

        #region Html Extensions

        /// <summary>
        /// 通用标签
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="renderFormControlClass"></param>
        /// <returns></returns>
        public static MvcHtmlString LayUiEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression, bool renderFormControlClass = true, string placeholder = "")
        {
            var result = new StringBuilder();

            var htmlAttributes = new
            {
                @class = renderFormControlClass ? "layui-input" : "",
            };
            result.Append(helper.EditorFor(expression, new { htmlAttributes, placeholder }));

            return MvcHtmlString.Create(result.ToString());
        }


        public static MvcHtmlString LayUiTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var result = new StringBuilder();

            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attrs.Add("class", "layui-textarea");

            result.Append(helper.TextAreaFor(expression, attrs));

            return MvcHtmlString.Create(result.ToString());
        }
        /// <summary>
        /// MuiLabel
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="displayHint"></param>
        /// <returns></returns>
        public static MvcHtmlString LayUiLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, bool displayHint = true)
        {
            return helper.LabelFor(expression, new { @class = "layui-form-label" });
        }


        public static MvcHtmlString LayuiCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object htmlAttributes = null, string layfilter = null)
        {
            var attributes =
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (String.IsNullOrWhiteSpace(layfilter))
            {
                var htmlName = CommonHtmlExtensions.FieldIdFor(htmlHelper, expression);
                attributes["lay-filter"] = htmlName;
            }
            else
                attributes["lay-filter"] = layfilter;

            return htmlHelper.CheckBoxFor(expression, attributes);

        }


        public static MvcHtmlString LayuiDateTimeFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,string placeholder = "yyyy-MM-dd")
        {
            var htmlAttributes = new
            {
                placeholder = placeholder,
                @class = "layui-input",
                @readonly = "readonly"
            };
            return htmlHelper.TextBoxFor(expression, htmlAttributes);

        }

        public static MvcHtmlString LayuiDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> itemList,
            object htmlAttributes = null, string optionLabel = null, string layfilter = null)
        {
            var result = new StringBuilder();

            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attrs.Add("class", "layui-input layui-unselect");
            if (!String.IsNullOrWhiteSpace(layfilter))
                attrs.Add("lay-filter", layfilter);
            
                result.Append(helper.DropDownListFor(expression, itemList, optionLabel, attrs).ToHtmlString());


            return MvcHtmlString.Create(result.ToString());
        }

        #endregion
    }
}