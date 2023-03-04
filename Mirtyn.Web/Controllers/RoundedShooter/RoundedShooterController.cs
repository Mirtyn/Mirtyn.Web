using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mirtyn.Web;
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
        public IActionResult Post(IFormCollection collection, Ladder.Entry entry, string version)
        {
            Logger.LogDebug("Post: " + collection["flag"] + " version: " + version);
            Logger.LogDebug("Post: " + entry.Flag.ToString());
            Logger.LogDebug("Post: " + collection["points"]);

            var points = long.Parse(collection["points"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);

            entry.Points = points;

            //if ("Beta 0.1.0".Equals(version, StringComparison.InvariantCultureIgnoreCase) || "0.1.0".Equals(version, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return PostV010(collection, entry);
            //}

            return PostV020(collection, entry);
        }

        [HttpPost]
        [Route("ladder/v0.2.0/post")]
        public IActionResult PostV020(IFormCollection collection, Ladder.Entry entry)
        {
            Logger.LogDebug("PostV011: " + entry.Flag.ToString());

            Logger.LogDebug("entry.Flag: " + entry.Flag.ToString());

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
