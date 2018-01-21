using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml;

namespace NetCommunitySolution.Web.Framework.Htmls
{
    public static class CommonHtmlExtensions
    {

        /// <summary>
        /// Id绑定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string FieldIdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return id.Replace('[', '_').Replace(']', '_');
        }

        public static string GetUrlAddress(this string htmlString,string parameter)
        {
            Regex reg = new Regex(parameter);
            return reg.Match(htmlString).Value;
        }

    }
}