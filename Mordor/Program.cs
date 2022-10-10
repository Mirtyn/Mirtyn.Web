using System;
using System.Collections.Generic;
using System.Threading;

namespace Mirtyn.Mordor
{
    public class RandomCL
    {
        static void Main(string[] args)
        {
            var OrcGenerator = new OrcGenerator();

            OrcGenerator.OrcGeneratorStart();
            


            Console.ReadKey();
        }

        


    }
    public class OrcCaptain
    {
        public string name = string.Empty;
        public string title = string.Empty;
        public int level = 0;
        public float maxhealth = 0;
        public float health = 0;
        //public object traits = new List<string>();
        public string[] traits = {
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        };
    }

    public class OrcGenerator
    {
        public void OrcGeneratorStart()
        {
            Random rnd = new Random();

            var orc = new OrcCaptain();

            orc.name = OrcNameGenerator(rnd);

            orc.title = OrcTitleGenerator(rnd);

            orc.level = OrcLevelGenerator(rnd);

            orc.maxhealth = OrcHealthGenerator(orc.level);

            orc.health = orc.maxhealth;

            orc.traits = OrcTraitsGenerator(rnd, orc.level);



            DisplayOrc(orc, orc.traits);
        }

        public string OrcNameGenerator(Random rnd)
        {
            string[] names = {
                "Ogg",
                "Ûkbûk",
                "Krimp",
                "Dûsh",
                "Goend",
                "Krûk"
            };
            int namesIndex = rnd.Next(names.Length);
            string name = (names[namesIndex]);

            return name;
        }

        public string OrcTitleGenerator(Random rnd)
        {
            string[] titles = {
                "The Bloated",
                "The Fat",
                "Of The Hand",
                "The Dung Collector",
                "Black-Heart",
                "The Blacksmith",
                "The Cannibal",
                "Dead Wood",
                "The Gab",
                "Itchy Feet",
                "The Eater",
                "Child Eater",
                "The Mindless"
            };
            int titlesIndex = rnd.Next(titles.Length);
            string title = (titles[titlesIndex]);

            return title;
        }

        public int OrcLevelGenerator(Random rnd)
        {
            var level = (rnd.Next(1, 26));

            //  Lvl         1    2-3   4-6   7-8   9-11  12-13 14-16 17-18 19-21 22-23 24-25
            //  Num Traits  0    1     2     3     4     5     6     7     8     9     10

            return level;
        }

        public int OrcHealthGenerator(int level)
        {
            int maxhealth = level * 10;

            return maxhealth;
        }

        public string[] OrcTraitsGenerator(Random rnd, int level)
        {
            //  Lvl         1    2-3   4-6   7-8   9-11  12-13 14-16 17-18 19-21 22-23 24-25
            //  Num Traits  0    1     2     3     4     5     6     7     8     9     10

            int LevelTraits = Convert.ToInt32(level / 2.5);

            var TraitsList = new List<string> {
                "Charge Attack",
                "Rapid Attack",
                "Leash Attack", 
                "Jump Attack", 
                "Axe Throw", 
                "Stun Bombs", 
                "Grab Throw",
                "Ground Slam", 
                "Body Slam",
                "Vault Breaker",
                "Regeneration",
                "Gang Leader",
                "Caragor Rider",
                "Beast Slayer",
                "Throwing Knifes"
            };

            if (LevelTraits > TraitsList.Count)
            {
                LevelTraits = TraitsList.Count;
            }

            var Traits = new List<string>();
            var TraitsCopy = new List<string>(TraitsList);

            for (var i = 0; Traits.Count < LevelTraits; i++)
            {
                var index = rnd.Next(TraitsCopy.Count);

                var trait = TraitsCopy[index];

                var exists = Traits.Exists(o => o == trait);

                if (!exists)
                {
                    Traits.Add(trait);

                    TraitsCopy.Remove(trait);
                }
            }
            string[] TraitsArray = Traits.ToArray();

            return TraitsArray;
        }
        
        public void DisplayOrc(OrcCaptain orc, string[] traits)
        {
            Console.WriteLine("\nOrc captains name and title:");
            Console.WriteLine(orc.name);
            Console.WriteLine(orc.title);

            Console.WriteLine("\nOrc captains level:");
            Console.WriteLine(orc.level);

            Console.WriteLine("\nOrc captains health:\n");
            Console.WriteLine(orc.maxhealth);

            Console.WriteLine("Orc captains traits:");
            for (int i = 0; i < orc.traits.Length; i++)
            {
                Console.WriteLine(orc.traits[i]);
            }
            if (orc.traits.Length == 0)
            {
                Console.WriteLine("He doesn't have any traits.");
            }

            Console.WriteLine("\n\n");
        }
    }
}
