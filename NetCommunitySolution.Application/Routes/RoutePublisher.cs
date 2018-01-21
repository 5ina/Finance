using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace NetCommunitySolution.Routes
{

    public class RoutePublisher : NetCommunitySolutionAppServiceBase,IRoutePublisher
    {        

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes</param>
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IRouteProvider))))
                .ToArray();

            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in types)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }
        public IEnumerable<Type> GetRouteClass(Type interfaceType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var t in type.GetInterfaces())
                    {
                        if (t == interfaceType)
                        {
                            yield return type;
                            break;
                        }
                    }
                }
            }
        }
    }
}
