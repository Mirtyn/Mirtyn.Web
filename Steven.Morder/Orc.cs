using System;
using System.Collections.Generic;
using System.Text;

namespace Steven.Morder
{
    public class Orc
    {
        public string Name;
        public string Title;
        public OrcType OrcType;
        public int Level;
        public int Life;

        public void ConsoleWrite()
        {
            Console.WriteLine($"{Name} {Title}");
            Console.WriteLine($"{OrcType} Level: {Level}");
            Console.WriteLine($"Life: {Life}");
            Console.WriteLine($"---");
        }
    }
}
