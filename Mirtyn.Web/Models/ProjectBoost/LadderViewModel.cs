using ProjectBoost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Mirtyn.Web
{
    public class LadderViewModel
    {
        public Ladder Ladder { get; set; }
        public List<Version> SavedLadderVersions { get; set; }
    }
}
