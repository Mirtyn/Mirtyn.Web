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
        //public List<string> WeakOrStrong = new List<string>();
        public List<string> Weaknesses = new List<string>();
        public List<string> Immunities = new List<string>();
        public List<string> Fears = new List<string>();


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

            Console.WriteLine("----------");

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

            Console.WriteLine("----------");

            Console.WriteLine("Fears: ");

            foreach (var fear in Fears)
            {
                Console.WriteLine(fear);
            }

            if (Fears.Count == 0)
            {
                Console.WriteLine("He doesn't have any fears.");
            }

            Console.WriteLine("----------");

            Console.WriteLine("Weaknesses: ");

            foreach (var weakness in Weaknesses)
            {
                Console.WriteLine(weakness);
            }

            if (Weaknesses.Count == 0)
            {
                Console.WriteLine("He doesn't have any weaknesses.");
            }

            Console.WriteLine("----------");

            Console.WriteLine("Immunities:");

            foreach (var immunity in Immunities)
            {
                Console.WriteLine(immunity);
            }

            if (Immunities.Count == 0)
            {
                Console.WriteLine("He doesn't have any immunities.");
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

            OrcStrongOrWeak(rnd, orc);

            //orc.Immunities = OrcImmunitiesGenerator();

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

        public void OrcStrongOrWeak(Random rnd, OrcCaptain orc)
        {
            var a = 1 + rnd.NextDouble() * 3.75;

            //if (a < 1)
            //{
            //    a = 1;
            //}

            var chance = orc.Level * a;

            var strenghtsOrWeaknessList = new List<string> {
                "Fire",
                "Poison",
                "Ice",
                "BaleFire"
            };

            //var strenghtsOrWeaknessListCopy = new List<string>(strenghtsOrWeaknessList);
            //var ctr = 0;
            var immunities = new List<string>();
            var weaknesses = new List<string>();
            var fears = new List<string>();

            if (chance <= 33)
            {
                for (var i = 0; i < strenghtsOrWeaknessList.Count; i++)
                {
                    var chancewhere = rnd.Next(0, 101);

                    var trait = strenghtsOrWeaknessList[i];

                    if (chancewhere <= 40)
                    {
                        //var index = rnd.Next(strenghtsOrWeaknessListCopy.Count);

                        //var exists = ctr.Exists(o => o == trait);

                        //ctr = ctr++;

                        weaknesses.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);

                        //if (!exists)
                        //{
                        //    ctr.Add(trait);

                        //    strenghtsOrWeaknessListCopy.Remove(trait);
                        //}
                    }
                    else if (chancewhere <= 70)
                    {
                        //var trait = strenghtsOrWeaknessListCopy[ctr];

                        //ctr = ctr++;

                        fears.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);
                    }
                    else if (chancewhere <= 92)
                    {
                        continue;
                    }
                    else
                    {
                        //var trait = strenghtsOrWeaknessListCopy[ctr];

                        //ctr = ctr++;

                        immunities.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);
                    }
                }
            }
            
            else if (chance <= 66)
            {
                for (var i = 0; i < strenghtsOrWeaknessList.Count; i++)
                {
                    var chancewhere = rnd.Next(0, 101);

                    if (chancewhere <= 30)
                    {
                        //var index = rnd.Next(strenghtsOrWeaknessListCopy.Count);

                        var trait = strenghtsOrWeaknessList[i];

                        //var exists = ctr.Exists(o => o == trait);

                        //ctr = ctr++;

                        weaknesses.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);

                        //if (!exists)
                        //{
                        //    ctr.Add(trait);

                        //    strenghtsOrWeaknessListCopy.Remove(trait);
                        //}
                    }
                    else if (chancewhere <= 45)
                    {
                        var trait = strenghtsOrWeaknessList[i];

                        //ctr = ctr++;

                        fears.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);
                    }
                    else if (chancewhere <= 80)
                    {
                        continue;
                    }
                    else
                    {
                        var trait = strenghtsOrWeaknessList[i];

                        //ctr = ctr++;

                        immunities.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);
                    }
                }
            }
            
            else
            {
                for (var i = 0; i < strenghtsOrWeaknessList.Count; i++)
                {
                    var chancewhere = rnd.Next(0, 101);

                    if (chancewhere <= 20)
                    {
                        //var index = rnd.Next(strenghtsOrWeaknessListCopy.Count);

                        var trait = strenghtsOrWeaknessList[i];

                        //var exists = ctr.Exists(o => o == trait);

                        //ctr = ctr++;

                        weaknesses.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);

                        //if (!exists)
                        //{
                        //    ctr.Add(trait);

                        //    strenghtsOrWeaknessListCopy.Remove(trait);
                        //}
                    }
                    else if (chancewhere <= 25)
                    {
                        var trait = strenghtsOrWeaknessList[i];

                        //ctr = ctr++;

                        fears.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);
                    }
                    else if (chancewhere <= 75)
                    {
                        continue;
                    }
                    else
                    {
                        var trait = strenghtsOrWeaknessList[i];

                        //ctr = ctr++;

                        immunities.Add(trait);

                        //strenghtsOrWeaknessListCopy.Remove(trait);
                    }
                }
            }
            orc.Fears = fears;
            orc.Weaknesses = weaknesses;
            orc.Immunities = immunities;
        }

        //public void OrcImmunitiesGenerator()
        //{

        //}

        //public void OrcFearsGenerator()
        //{

        //}
    }
}
