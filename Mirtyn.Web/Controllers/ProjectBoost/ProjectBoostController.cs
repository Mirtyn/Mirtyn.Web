using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mirtyn.Web;
using ProjectBoost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mirtyn.Web
{
    public class ProjectBoostController : AbstractProjectController<ProjectBoostController>
    {
        private string _dataPath;

        public ProjectBoostController()
        {
            _dataPath = Project.MapPath("~/data/project-boost/ladders/");
        }

        [Route("ladder")]
        public IActionResult IndexRedirect()
        {
            return RedirectToActionPermanent("Index");
        }

        [Route("project-boost")]
        [Route("project-boost/ladder")]
        public IActionResult Index()
        {
            var service = new ProjectBoostServerApi(_dataPath);

            var ladder = service.LoadLatest();

            return RedirectToAction("Ladder", new { version = ladder.Version });

            //var model = new LadderViewModel
            //{
            //    Ladder = ladder,
            //    SavedLadderVersions = service.EnumerateSavedLadderVersions().ToList(),
            //};

            //if (model.Ladder != null)
            //{
            //    model.SavedLadderVersions.RemoveAll(o => o == model.Ladder.Version);
            //}

            //return View(model);
        }

        [Route("ladder/{version}")]
        public IActionResult LadderRedirect(string version)
        {
            return RedirectToActionPermanent("Ladder", new { version });
        }

        [Route("project-boost/ladder/{version}")]
        public IActionResult Ladder(string version)
        {
            var service = new ProjectBoostServerApi(_dataPath);

            var ladder = service.Load(version);

            var model = new LadderViewModel
            {
                Ladder = ladder,
                SavedLadderVersions = service.EnumerateSavedLadderVersions().ToList(),
            };

            if (model.Ladder != null)
            {
                model.SavedLadderVersions.RemoveAll(o => o == model.Ladder.Version);
            }

            return View(model);
        }

        [Route("ladder/post")]
        public IActionResult PostRedirect()
        {
            return RedirectToActionPermanent("Post");
        }

        [Route("project-boost/ladder/post")]
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        [Route("project-boost/ladder/post")]
        [Route("ladder/post")]
        public IActionResult Post(IFormCollection collection, Ladder.Entry entry, string version)
        {
            Logger.LogDebug("Post: " + collection["flag"] + " version: " + version);
            Logger.LogDebug("Post: " + entry.Flag.ToString());
            Logger.LogDebug("Post: " + collection["time"]);

            var time = float.Parse(collection["time"].ToString().Replace(",", "."), CultureInfo.InvariantCulture);

            entry.Time = time;

            if ("Beta 0.1.0".Equals(version, StringComparison.InvariantCultureIgnoreCase) || "0.1.0".Equals(version, StringComparison.InvariantCultureIgnoreCase))
            {
                return PostV010(collection, entry);
            }

            return PostV011(collection, entry);
        }

        [HttpPost]
        [Route("project-boost/ladder/v0.1.0/post")]
        [Route("ladder/v0.1.0/post")]
        public IActionResult PostV010(IFormCollection collection, Ladder.Entry entry)
        {
            Logger.LogDebug("PostV010: " + entry.Flag.ToString());

            var service = new ProjectBoostServerApi(_dataPath);

            var response = service.Save(entry, "0.1.0");

            return Json(response);
        }

        [HttpPost]
        [Route("project-boost/ladder/v0.1.1/post")]
        [Route("ladder/v0.1.1/post")]
        public IActionResult PostV011(IFormCollection collection, Ladder.Entry entry)
        {
            Logger.LogDebug("PostV011: " + entry.Flag.ToString());

            Logger.LogDebug("entry.Flag: " + entry.Flag.ToString());

            var service = new ProjectBoostServerApi(_dataPath);

            var response = service.Save(entry, "0.1.1");

            return Json(response);
        }

        [Route("project-boost/ladder/0.1.0/post/test")]
        public IActionResult PostTestV010()
        {
            var random = new Random();

            var now = DateTime.Now.ToString("U");

            var version = "0.1.0";

            var entry = new Ladder.Entry
            {
                Name = now,
                TimeInSeconds = 20f + 40f * (float)random.NextDouble()
            };

            var service = new LadderClientApi(Configuration.GetValue<string>("ProjectBoostLadderPostUrl"));

            service.TryPost(entry, version, out LadderClientApi.PostResponse response);

            return RedirectToAction("Post");
        }

        [Route("project-boost/ladder/0.1.1/post/test")]
        public IActionResult PostTestV011()
        {
            var random = new Random();

            var now = DateTime.Now.ToString("U");

            var version = "0.1.1";

            var entry = new Ladder.Entry
            {
                Name = now,
                TimeInSeconds = 20f + 40f * (float)random.NextDouble(),
                Flag = ProjectBoost.Ladder.EntryFlag.Competitive,
            };

            var service = new LadderClientApi(Configuration.GetValue<string>("ProjectBoostLadderPostUrl"));

            service.TryPost(entry, version, out LadderClientApi.PostResponse response);




            now = DateTime.Now.ToString("U");

            entry = new Ladder.Entry
            {
                Name = now,
                TimeInSeconds = 20f + 40f * (float)random.NextDouble(),
                Flag = ProjectBoost.Ladder.EntryFlag.Competitive | ProjectBoost.Ladder.EntryFlag.RealLanding,
            };

            service.TryPost(entry, version, out response);




            now = DateTime.Now.ToString("U");

            entry = new Ladder.Entry
            {
                Name = now,
                TimeInSeconds = 20f + 40f * (float)random.NextDouble(),
                Flag = ProjectBoost.Ladder.EntryFlag.Competitive | ProjectBoost.Ladder.EntryFlag.OneLife,
            };

            service.TryPost(entry, version, out response);




            now = DateTime.Now.ToString("U");

            entry = new Ladder.Entry
            {
                Name = now,
                TimeInSeconds = 20f + 40f * (float)random.NextDouble(),
                Flag = ProjectBoost.Ladder.EntryFlag.Competitive | ProjectBoost.Ladder.EntryFlag.RealLanding | ProjectBoost.Ladder.EntryFlag.OneLife,
            };

            service.TryPost(entry, version, out response);

            return RedirectToAction("Post");
        }
    }
}
