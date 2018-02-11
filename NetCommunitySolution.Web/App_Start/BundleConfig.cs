using System.Web.Optimization;

namespace NetCommunitySolution.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Admin
            bundles.Add(
                new StyleBundle("~/admin/css")
                    .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
                    .Include("~/layui/css/admin.css", new CssRewriteUrlTransform())
                    .Include("~/layui/css/layui.css", new CssRewriteUrlTransform())
                    .Include("~/Content/admin.css", new CssRewriteUrlTransform())
                    .Include("~/Content/toastr.min.css")
                    .Include("~/Scripts/sweetalert/sweet-alert.css")
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                );


            //~/Operation
            bundles.Add(
                new StyleBundle("~/operation/css")
                    .Include("~/Content/themes/base/all.css")
                    .Include("~/Content/style.css")
                    .Include("~/Content/weui.min.css")
                    .Include("~/Content/toastr.min.css")
                    .Include("~/Scripts/sweetalert/sweet-alert.css")
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                );
            

            bundles.Add(
                new ScriptBundle("~/Bundles/js/top")
                    .Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/modernizr-2.8.3.js"
                    )
                );
            

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/js")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",

                        "~/Scripts/layui.all.js",
                        "~/Scripts/admin.common.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js"
                    )
                );

            bundles.Add(
                new ScriptBundle("~/layui/js")
                    .Include(
                        "~/layui/layui.all.js",
                        "~/Scripts/admin.common.js"
                    )
                );

        }
    }
}