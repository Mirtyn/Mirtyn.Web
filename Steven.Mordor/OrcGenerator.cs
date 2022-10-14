using System;
using System.Collections.Generic;
using System.Text;

namespace Steven.Morder
{
    public class OrcGeneratorSettings
    {
        public int Count;
        public OrcType OrcType;
        public int MinLevel = 1;
        public int MaxLevel = 32;
    }

    public enum OrcType : int
    {
        Unknown = 0,
        Soldier,
        Captain,
        Warlord,
    }

    public class OrcGenerator
    {
        private static Random Random = new Random();
        private static List<string> Names = new List<string>
        {
            "tul",
            "mü",
            "hi",
            "aët",
            "pom",
            "klu",
            "ush",
            "praë",
        };
        private static List<string> Titles = new List<string>
        {
            "The Cruel",
            "The Stupid",
            "The Meek",
            "Lion Killer",
            "Doom Slayer",
            "Meat Lover",
            "Bone Crusher",
            "Venom Licker",
            "Burn Face",
            "Skull Ripper",
        };

        public IEnumerable<Orc> Generate(OrcGeneratorSettings settings)
        {
            var orcs = new List<Orc>();

            for(var i = 0; i < settings.Count; i++)
            {
                var orc = GenerateOrc(settings);
                
                orcs.Add(orc);
            }

            return orcs;
        }

        private Orc GenerateOrc(OrcGeneratorSettings settings)
        {
            var orc = new Orc();

            orc.OrcType = settings.OrcType;
            orc.Level = Random.Next(settings.MinLevel, settings.MaxLevel + 1);
            orc.Life = GenerateOrcLife(orc);
            orc.Name = GenerateOrcName();
            orc.Title = Titles[Random.Next(Titles.Count)];

            return orc;
        }

        private int GenerateOrcLife(Orc orc)
        {
            var upperlevel = (int)(orc.Level * 0.10);

            return Random.Next(orc.Level * 10, (orc.Level + upperlevel) * 10);
        }

        private string GenerateOrcName()
        {
            var name = Names[Random.Next(Names.Count)] + Names[Random.Next(Names.Count)];

            return name[0].ToString().ToUpper() + name.Substring(1);
        }
    }
}
