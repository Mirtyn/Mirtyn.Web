using System;

namespace Steven.Morder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var orcGenerator = new OrcGenerator();

            var orcGeneratorSettings = new OrcGeneratorSettings
            {
                Count = 8,
                OrcType = OrcType.Warlord,
                MinLevel = 24,
                MaxLevel = 64,
            };

            var orcs = orcGenerator.Generate(orcGeneratorSettings);

            foreach(var orc in orcs)
            {
                orc.ConsoleWrite();
            }

            Console.WriteLine("Press a key to quit");
            Console.ReadKey(false);
        }
    }
}
