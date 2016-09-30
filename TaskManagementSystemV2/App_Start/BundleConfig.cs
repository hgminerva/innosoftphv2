using System.Web;
using System.Web.Optimization;

namespace TaskManagementSystemV2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-dropdownhover.min.js",
                      "~/Scripts/respond.min.js",
                      "~/Scripts/jquery.js",
                      "~/Scripts/jquery.inview.min.js",
                      "~/Scripts/wow.min.js",
                      "~/Scripts/mousescroll.js",
                      "~/Scripts/smoothscroll.js",
                      "~/Scripts/jquery.countTo.js",
                      "~/Scripts/owl.carousel.js",
                      "~/Scripts/toastr.min.js",
                      "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/bootstrap-dropdown.min.css",
                      "~/fonts/font-awesome-4.6.1/css/font-awesome.min.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/owl.theme.css",
                      "~/Content/owl.transitions.css",
                      "~/wijmo/styles/wijmo.min.css",
                      "~/Content/responsive.css",
                      "~/Content/toastr.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/software-js").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/wijmo/controls/wijmo.min.js",
                      "~/wijmo/controls/wijmo.input.min.js",
                      "~/wijmo/controls/wijmo.grid.min.js",
                      "~/wijmo/controls/wijmo.chart.min.js",
                      "~/Scripts/toastr.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
