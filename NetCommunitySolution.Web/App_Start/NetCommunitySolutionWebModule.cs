using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Web.Mvc;
using NetCommunitySolution.Routes;

namespace NetCommunitySolution.Web
{
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(NetCommunitySolutionDataModule), 
        typeof(NetCommunitySolutionApplicationModule), 
        typeof(NetCommunitySolutionWebApiModule),
        typeof(AbpAutoMapperModule))]
    public class NetCommunitySolutionWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn"));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    NetCommunitySolutionConsts.LocalizationSourceName,
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Localization/NetCommunitySolution")
                        )
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<NetCommunitySolutionNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;


        }
        
    }
}
