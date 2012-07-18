using System.Web;
using System.Web.Optimization;

namespace Br.MetroStyleFriends.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout-2.1.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/cufon-yui.js",
                        "~/Scripts/segoe_400.font.js",
                        "~/Scripts/Common.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/bootstrap.min.css"
                        ));
        }
    }
}