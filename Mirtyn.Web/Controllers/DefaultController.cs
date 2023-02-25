using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mirtyn.Web
{
    public class DefaultController : AbstractProjectController<DefaultController>
    {
        public IActionResult Index()
        {
            if (Request.Host.HasValue && Request.Host.Value.Contains("project-boost", StringComparison.InvariantCultureIgnoreCase))
            {
                return RedirectPermanent("https://mirtyn.be/project-boost/ladder");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}