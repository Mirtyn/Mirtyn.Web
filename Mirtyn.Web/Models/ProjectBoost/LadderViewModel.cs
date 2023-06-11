using ProjectBoost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using static ProjectBoost.Ladder;

namespace Mirtyn.Web
{
    public class LadderViewModel
    {
        public List<WorldFlag> WorldFlags { get; set; }

        public WorldFlag ActiveWorldFlag { get; set; } = WorldFlag.World1;

        public Ladder Ladder { get; set; }
        public List<Version> SavedLadderVersions { get; set; }
    }

    public class LadderViewModel<TLadder>
    {
        public TLadder? Ladder { get; set; }
        public List<Version>? SavedLadderVersions { get; set; }
    }
}
