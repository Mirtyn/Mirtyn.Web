using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mirtyn.Web;
using Newtonsoft.Json;
using RoundedShooter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mirtyn.Web
{
    [Route("rounded-shooter")]
    public class RoundedShooterController : AbstractProjectController<RoundedShooterController>
    {
        private RoundedShooterServerApi _service;

        public RoundedShooterController()
        {
            _service = new RoundedShooterServerApi(Project.MapPath("~/data/rounded-shooter/ladders/"));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewData["HeaderTitle"] = "Rounded Shooter: a game developed by Mirtyn";
            ViewData["HeaderDescription"] = "Rounded Shooter: a game where you defeat waves of enemies as it gets increasingly harder. Rounded Shooter is developed by Mirtyn";
        }

        [Route("")]
        [Route("ladder")]
        public IActionResult Index()
        {
            var ladder = _service.LoadLatest();

            return RedirectToAction("Ladder", new { version = ladder != null ? ladder.Version : new Version() });
        }

        [Route("ladder/{version}")]
        public IActionResult Ladder(string version)
        {
            var ladder = _service.Load(version);

            var model = new LadderViewModel<RoundedShooter.Ladder>
            {
                Ladder = ladder,
                SavedLadderVersions = _service.EnumerateSavedLadderVersions().ToList(),
            };

            if (model.Ladder != null)
            {
                model.SavedLadderVersions.RemoveAll(o => o == model.Ladder.Version);
            }

            return View(model);
        }

        [Route("ladder/post")]
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        [Route("ladder/post")]
        public IActionResult Post(IFormCollection collection, string json, string version)
        {
            var entry = JsonConvert.DeserializeObject<Ladder.Entry> (json);

            Logger.LogDebug("Post: " + entry.Flag.ToString() + " version: " + version);
            Logger.LogDebug("Post: " + collection["points"]);

            var response = _service.Save(entry, version);

            return Json(response);

        }

        [HttpPost]
        [Route("ladder/v0.2.0/post")]
        public IActionResult PostV020(IFormCollection collection, Ladder.Entry entry)
        {
            Logger.LogDebug("Post v 0.2.0");

            Logger.LogDebug("Flag: " + entry.Flag.ToString());

            var response = _service.Save(entry, "0.2.0");

            return Json(response);
        }

        [Route("ladder/0.2.0/post/test")]
        public IActionResult PostTestV020()
        {
            var random = new Random();

            var now = DateTime.Now.ToString("U");

            var version = "0.2.0";

            var entry = new Ladder.Entry
            {
                Name = now,
                Points = 20000 + (long)(40000 * (float)random.NextDouble())
            };

            var service = new LadderClientApi(Configuration.GetValue<string>("RoundedShooterLadderPostUrl"));

            service.TryPost(entry, version, out LadderClientApi.PostResponse response);

            return RedirectToAction("Post");
        }
    }
}
