using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBoostLadder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoostLadder.Controllers
{
    public class LadderController : ProjectController<LadderController>
    {
        [HttpPost]
        [Route("ladder/post")]
        public IActionResult Post(IFormCollection collection, string name, float time, string version)
        {
            var entry = new Ladder.Entry
            {
                Name = name,
                TimeInSeconds = time
            };

            var service = new LadderServerApi(Project.MapPath("~/data/ladders/"));

            var response = service.Save(entry, version);

            return Json(response);
        }
    }
}
