using NetCommunitySolution.Seo;
using NetCommunitySolution.Web.Framework.Localization;
using System.Web;
using System.Web.Routing;

namespace NetCommunitySolution.Web.Framework.Seo
{

    public partial class GenericPathRoute : LocalizedRoute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class and default parameter values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values and constraints.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values, 
        /// constraints,and custom values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. The route handler might need these values to process the request.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData data = base.GetRouteData(httpContext);
            if (data != null )
            {
                var urlRecordService = Abp.Dependency.IocManager.Instance.Resolve<IUrlRecordService>();
                var slug = data.Values["generic_se_name"] as string;
                //performance optimization.
                //we load a cached verion here. it reduces number of SQL requests for each page load
                var urlRecord = urlRecordService.GetBySlugCached(slug);
                //comment the line above and uncomment the line below in order to disable this performance "workaround"
                //var urlRecord = urlRecordService.GetBySlug(slug);
                if (urlRecord == null)
                {
                    //no URL record found

                    //var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    //var response = httpContext.Response;
                    //response.Status = "302 Found";
                    //response.RedirectLocation = webHelper.GetStoreLocation(false);
                    //response.End();
                    //return null;

                    data.Values["controller"] = "Topic";
                    data.Values["action"] = "NoTopic";
                    return data;
                }
                //ensure that URL record is active
                if (!urlRecord.IsActive)
                {
                    //URL record is not active. let's find the latest one
                    var activeSlug = urlRecordService.GetActiveSlug(urlRecord.EntityId, urlRecord.EntityName);
                    if (string.IsNullOrWhiteSpace(activeSlug))
                    {
                        data.Values["controller"] = "Topic";
                        data.Values["action"] = "NoTopic";
                        return data;
                    }
                    

                    var response = httpContext.Response;
                    response.Status = "301 Moved Permanently";
                    response.RedirectLocation = string.Format("{0}/{1}", httpContext.Request.Url.Host, activeSlug);
                    response.End();
                    return null;
                }

                //process URL
                switch (urlRecord.EntityName.ToLowerInvariant())
                {
                    case "topic":
                        {
                            data.Values["controller"] = "Topic";
                            data.Values["action"] = "TopicBlock";
                            data.Values["topicId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
            }
            return data;
        }

        #endregion
    }
}