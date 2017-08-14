using System.Web.Optimization;

namespace Portfolio
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var styleBundle = new StyleBundle("~/Client/Styles/Bundled")
    .Include("~/Client/Styles/Bundled.css");

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Styles/Styles.css"));

            bundles.Add(new ScriptBundle("~/Content/javascript").Include(
                "~/Content/Scripts/jquery-{version}.js",
                "~/Content/Scripts/jquery.validate*",
                "~/Content/Scripts/modernizr-*",
                "~/Content/Scripts/bootstrap.js",
                "~/Content/Scripts/respond.js",
                "~/Content/Scripts/angular.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
