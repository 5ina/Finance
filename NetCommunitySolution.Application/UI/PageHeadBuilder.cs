using NetCommunitySolution.Common;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace NetCommunitySolution.UI
{
    public class PageHeadBuilder : NetCommunitySolutionAppServiceBase, IPageHeadBuilder
    {

        #region Fields
        

        private readonly SeoSetting _setting;

        #endregion

        #region Ctor

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="seoSettings">SEO settings</param>
        public PageHeadBuilder(ISettingService settingService)
        {
            this._setting = settingService.GetSeoSetting();
        }
        
        #endregion

        #region Utilities

        protected virtual string GetBundleVirtualPath(string prefix, string extension, string[] parts)
        {
            if (parts == null || parts.Length == 0)
                throw new ArgumentException("parts");

            //calculate hash
            var hash = "";
            using (SHA256 sha = new SHA256Managed())
            {
                // string concatenation
                var hashInput = "";
                foreach (var part in parts)
                {
                    hashInput += part;
                    hashInput += ",";
                }

                byte[] input = sha.ComputeHash(Encoding.Unicode.GetBytes(hashInput));
                hash = HttpServerUtility.UrlTokenEncode(input);
            }
            //ensure only valid chars
            hash = SeoExtensions.GetSeName(hash);

            var sb = new StringBuilder(prefix);
            sb.Append(hash);
            return sb.ToString();
        }

        protected virtual IItemTransform GetCssTranform()
        {
            return new CssRewriteUrlTransform();
        }

        #endregion

        #region method
        public string GenerateMetaDescription()
        {
            return _setting.DefaultMetaDescription;
        }

        public string GenerateMetaKeywords()
        {
            return _setting.DefaultMetaKeywords;
        }
        
        public string GenerateTitle()
        {
            return _setting.DefaultTitle;
        }

        #endregion
                
    }
}
