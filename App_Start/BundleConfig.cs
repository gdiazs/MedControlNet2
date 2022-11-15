using System.Web;
using System.Web.Optimization;

namespace MedControlNet
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));


            bundles.Add(new Bundle("~/bundles/knockout").Include(
                      "~/Scripts/knockout-3.5.1.js"));

            bundles.Add(new Bundle("~/bundles/api").Include(
          "~/Scripts/Api.js"));

            bundles.Add(new Bundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
        }
    }
}
