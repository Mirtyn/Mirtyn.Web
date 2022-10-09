using System;
using System.Threading;

namespace Mirtyn.Mordor
{
    public class RandomCL
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Random rndNames = new Random();
            Random rndTitles = new Random();
            Random rndLevel = new Random();
            Random rndLevel2 = new Random();

            //for (uint ctr = 1; ctr <= 10; ctr++)
            //    Console.WriteLine($"{rnd.Next(),15:N0}");

            var RandomSeed = (rnd.Next());

            //UpperBoundRandoms(RandomSeed, 25);

            string[] names = { "Ogg", "Ûkbûk", "Krimp", "Dûsh", "Goend", "Krûk" };
            string[] titles = { "The Bloated", "The Fat", "Of The Hand", "The Dung Collector", "Black-Heart",
                "The Blacksmith", "The Cannibal", "Dead Wood", "The Gab", "Itchy Feet",
                "The Eater", "Child Eater", "The Mindless" };

            int namesIndex = rndNames.Next(names.Length);
            int titlesIndex = rndTitles.Next(titles.Length);

            Console.WriteLine(names[namesIndex]);
            Console.WriteLine(titles[titlesIndex]);

            var Level = (rndLevel.Next(1,26));

            //  Lvl         1    2-3   4-6   7-8   9-11  12-13 14-16 17-18 19-21 22-23 24-25
            //  Num Traits  0    1     2     3     4     5     6     7     8     9     10

            Console.WriteLine(Level);

            string[] traits = { "" };

            int LevelTraits = Convert.ToInt32(Level / 2.5);


            for (int i = 0; i < LevelTraits; i++)
            {
                Console.WriteLine(LevelTraits);
            }



            //Console.WriteLine(RandomSeed);




            Console.ReadKey();
        }
        // Generate random numbers with an upper bound specified.
        static void UpperBoundRandoms(int seed, int upper)
        {
            Console.WriteLine(
                "\nRandom object, seed = {0}, upper bound = {1}:",
                seed, upper);
            Random randObj = new Random(seed);
        
            // Generate six random integers from 0 to the upper bound.
            for (int j = 0; j < 6; j++)
                Console.Write("{0,11} ", randObj.Next(upper));
            Console.WriteLine();
        }
    }
}
