using System.Web;
using System.Web.Optimization;

namespace boonservice
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/content/icons/fuse-icon-font/style.css",
                      "~/content/vendor/animate.css/animate.min.css",
                      "~/content/vendor/pnotify/pnotify.custom.min.css",
                      "~/content/vendor/nvd3/build/nv.d3.min.css",
                      "~/content/vendor/perfect-scrollbar/css/perfect-scrollbar.min.css",
                      "~/content/vendor/angular/loading-bar.css",
                      "~/content/vendor/fuse-html/fuse-html.min.css",
                      "~/content/css/main.css",
                      "~/content/site.css",
                      "~/content/vendor/magnific-popup/magnific-popup.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/content/vendor/jquery/dist/jquery.min.js",
                        "~/content/vendor/mobile-detect/mobile-detect.min.js",
                        "~/content/vendor/perfect-scrollbar/js/perfect-scrollbar.jquery.min.js",
                        "~/content/vendor/popper.js/index.js",
                        "~/content/vendor/bootstrap/bootstrap.min.js",
                        "~/content/vendor/d3/d3.min.js",
                        "~/content/vendor/nvd3/build/nv.d3.min.js",
                        "~/content/vendor/datatables.net/js/jquery.dataTables.min.js",
                        "~/content/vendor/datatables-responsive/js/dataTables.responsive.js",
                        "~/content/vendor/pnotify/pnotify.custom.min.js",
                        "~/content/vendor/lodash/lodash.js",
                        "~/content/vendor/magnific-popup/jquery.magnific-popup.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/content/vendor/angular/angular.min.js",
                        "~/content/vendor/angular/angular-animate.min.js",
                        "~/content/vendor/angular/angular-cookies.min.js",
                        "~/content/vendor/angular/angular-sanitize.min.js",
                        "~/content/vendor/angular/angular-touch.min.js",
                        "~/content/vendor/angular/angular-route.min.js",
                        "~/content/vendor/angular/angular-idle.min.js",
                        "~/content/vendor/angular/angular-ui-router.min.js",
                        "~/content/vendor/angular/loading-bar.js",
                        "~/content/vendor/ngMask/ngMask.min.js",
                        "~/content/vendor/ocLazyLoad/ocLazyLoad.min.js",
                        "~/content/vendor/angular-base64-upload/angular-base64-upload.min.js"));
        }
    }
}
