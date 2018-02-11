using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NetCommunitySolution.Web.Framework.Htmls
{
    /// <summary>
    /// Raozor扩展标签语言
    /// </summary>
    public static class HtmlExtensions
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
        public static MvcHtmlString WEEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression, string placeholder = "", string pattern = null)
        {
            var result = new StringBuilder();

            var htmlAttributes = new
            {
                @class =  "weui-input" ,
                placeholder = placeholder,
                pattern = pattern,
            };            

            result.Append(helper.EditorFor(expression, new { htmlAttributes }));

            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString WEEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression, bool required,string emptytips= null,
           string notmatchtips= null , string pattern = null)
        {
            var result = new StringBuilder();

            var htmlAttributes = new
            {
                @class = "weui-input",
                required = required,
                emptytips = emptytips,
                notmatchtips = notmatchtips,
                pattern = pattern
            };

            result.Append(helper.EditorFor(expression, new { htmlAttributes }));

            return MvcHtmlString.Create(result.ToString());
        }


        public static MvcHtmlString WETextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, string placeholder = "")
        {
            return helper.TextBoxFor(expression, new { @class = "weui-input", placeholder = placeholder });
        }
        public static MvcHtmlString WEReadOnlyFor<TModel, TValue>(this HtmlHelper<TModel> helper,
                Expression<Func<TModel, TValue>> expression, string placeholder = "")
        {
            return helper.TextBoxFor(expression, new { @class = "weui-input", @readonly = "readonly" });
            }


        /// <summary>
        ///数字文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="placeholder"></param>
        /// <param name="pattern"></param>
        /// <param name="required"></param>
        /// <param name="maxLength"></param>
        /// <param name="emptytips"></param>
        /// <param name="notmatchtips"></param>
        /// <returns></returns>
        public static MvcHtmlString WENumberFor<TModel, TValue>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression, string defaultValue = "", string placeholder = "",
           string pattern = null, bool required = false, int? maxLength = null,string emptytips =null,
           string notmatchtips = null)
        {
            var result = new StringBuilder();

            var requiredstr = "";
            if (required)
                requiredstr = " required='required'";

            var maxLengthstr = "";
            if (maxLength.HasValue)
                maxLengthstr = " maxlength='" + maxLength.Value + "'";

            var etips = "";
            if (!String.IsNullOrWhiteSpace(emptytips))
                etips = " emptytips='" + emptytips + "'";

            var ntips = "";
            if (!String.IsNullOrWhiteSpace(notmatchtips))
                ntips = " notmatchtips='" + notmatchtips + "'";
            
            result.Append("<input type='number' id='" + helper.FieldIdFor(expression)
                            + "' name='" + helper.FieldIdFor(expression)
                            + "' class='weui-input' pattern='" + pattern
                            + "' placeholder='" + placeholder
                            + "'" + requiredstr + maxLengthstr
                            + etips + ntips + "value='" + defaultValue + "' />");

            

            return MvcHtmlString.Create(result.ToString());
        }


        public static MvcHtmlString WEDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> itemList)
        {
            var htmlAttributes = new
            {
                @class = "weui-select",
            };

            return helper.DropDownListFor(expression, itemList, htmlAttributes);
            
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
        public static MvcHtmlString WELabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return helper.LabelFor(expression, new { @class = "weui-label" });
        }

        public static MvcHtmlString WEPasswordFor<TModel, TValue>(this HtmlHelper<TModel> helper,
                Expression<Func<TModel, TValue>> expression,string placeholder="")
        {

            return helper.PasswordFor(expression, new { @class = "weui-input", @required = "required", @placeholder = placeholder });

        }
        #endregion

    }
}