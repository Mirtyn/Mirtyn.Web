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

            var quit = false;

            while(!quit)
            {
                //OrcGenerator.OrcGeneratorStart();
                OrcGenerator.OrcGeneratorStart((int)DateTime.Now.Ticks);

                var key = Console.ReadKey();

                if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                {
                    quit = true;
                }

                Console.Clear();
            }
        }

        


    }
    public class OrcCaptain
    {
        public string Name = string.Empty;
        public string Title = string.Empty;
        public int Level = 0;
        public float MaxHealth = 0;
        public float Health = 0;
        public List<string> Traits = new List<string>();

        //public string[] traits = {
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty,
        //    string.Empty
        //};

        public void DisplayOrc()
        {
            Console.WriteLine($"Orc captain : {Name} {Title}");

            Console.WriteLine("Level:" + Level);

            Console.WriteLine("Health: " + MaxHealth);

            Console.WriteLine("Traits:");
            //for (int i = 0; i < orc.traits.Count; i++)
            //{
            //    Console.WriteLine(orc.traits[i]);
            //}

            foreach (var trait in Traits)
            {
                Console.WriteLine(trait);
            }

            if (Traits.Count == 0)
            {
                Console.WriteLine("He doesn't have any traits.");
            }

            Console.WriteLine("\n\n");
        }
    }

    public class OrcGenerator
    {
        public void OrcGeneratorStart(int seed = 1)
        {
            Random rnd = new Random(seed);

            var orc = new OrcCaptain();

            orc.Name = OrcNameGenerator(rnd);

            orc.Title = OrcTitleGenerator(rnd);

            orc.Level = OrcLevelGenerator(rnd);

            orc.MaxHealth = OrcHealthGenerator(orc.Level);

            orc.Health = orc.MaxHealth;

            orc.Traits = OrcTraitsGenerator(rnd, orc.Level);


            orc.DisplayOrc();
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

        public List<string> OrcTraitsGenerator(Random rnd, int level)
        {
            //  Lvl         1    2-3   4-6   7-8   9-11  12-13 14-16 17-18 19-21 22-23 24-25
            //  Num Traits  0    1     2     3     4     5     6     7     8     9     10

            int levelTraits = Convert.ToInt32(level / 2.5);

            var traitsList = new List<string> {
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

            if (levelTraits > traitsList.Count)
            {
                levelTraits = traitsList.Count;
            }

            var traits = new List<string>();
            var traitsCopy = new List<string>(traitsList);

            for (var i = 0; traits.Count < levelTraits; i++)
            {
                var index = rnd.Next(traitsCopy.Count);

                var trait = traitsCopy[index];

                var exists = traits.Exists(o => o == trait);

                if (!exists)
                {
                    traits.Add(trait);

                    traitsCopy.Remove(trait);
                }
            }
            //string[] TraitsArray = traits.ToArray();

            //return TraitsArray;

            return traits;
        }
    }
}
