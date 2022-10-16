using System;
using System.Collections.Generic;
using System.Linq;

namespace Steven.Morder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var orcGenerator = new OrcGenerator();

            var orcGeneratorSettings = new OrcGeneratorSettings
            {
                Count = 2,
                OrcType = OrcType.Warlord,
                MinLevel = 1,
                MaxLevel = 10,
            };

            var orcBattleSimulator = new OrcBattleSimulator();

            var quit = false;
            var attack = false;
            List<Orc> orcs = null;

            while (!quit)
            {
                if(!attack)
                {
                    orcs = orcGenerator.Generate(orcGeneratorSettings).ToList();

                    foreach (var orc in orcs)
                    {
                        orc.WriteInfoToConsole();
                    }
                }
                else
                {
                    orcBattleSimulator.Attack(orcs);
                    orcBattleSimulator.WriteLifeToConsole(orcs);

                    attack = false;
                }

                var key = Console.ReadKey();

                if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                {
                    quit = true;
                }

                if (key.KeyChar == 'a' || key.KeyChar == 'A')
                {
                    attack = true;
                }

                Console.Clear();
                Console.WriteLine("Press 'q' to quit, 'a' to attack or any other key to continue");
            }
        }
    }
}
