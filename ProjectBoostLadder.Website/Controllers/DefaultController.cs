using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBoost.Ladder.Website.Controllers.Abstracts;
using ProjectBoostLadder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoostLadder.Controllers
{
    public class DefaultController : AbstractProjectController<DefaultController>
    {
        public IActionResult Index()
        {
            var service = new LadderServerApi(Project.MapPath("~/data/ladders/"));

            var ladder = service.LoadLatest();

            var model = new LadderViewModel
            {
                Ladder = ladder,
                SavedLadderVersions = service.EnumerateSavedLadderVersions().ToList(),
            };

            if(model.Ladder != null)
            {
                model.SavedLadderVersions.RemoveAll(o => o == model.Ladder.Version);
            }

            return View(model);
        }

        [Route("Ladder/{version}")]
        public IActionResult Ladder(string version)
        {
            var service = new LadderServerApi(Project.MapPath("~/data/ladders/"));

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
