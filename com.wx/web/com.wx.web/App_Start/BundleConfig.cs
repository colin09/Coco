using System.Web;
using System.Web.Optimization;

namespace com.wx.web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jq-bs-av").Include(
                      "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/jquery.cxscroll.min.js",
                      "~/Scripts/jquery.lazyload.min.js",
                      "~/Scripts/avalon.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jq-bs-av-ng").Include(
                      "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/jquery.cxscroll.min.js",
                      "~/Scripts/jquery.lazyload.min.js",
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-sanitize.min.js",
                      "~/Scripts/avalon.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jq-bs-ng").Include(
                      "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/jquery.cxscroll.min.js",
                      "~/Scripts/jquery.lazyload.min.js",
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-sanitize.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/mgrCss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
