using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectBoostLadder
{
    public class Ladder
    {
        public Version Version { get; set; }

        public List<Entry> Entries { get; set; }

        public Ladder()
        {
            Entries = new List<Entry>();
        }

        public class Entry
        {
            public DateTime CreatedUtc { get; set; }

            public string Name { get; set; }

            public float TimeInSeconds { get; set; }

            public Entry()
            {
                CreatedUtc = DateTime.UtcNow;
            }
        }

        public int AddUniqueEntry(Entry entry)
        {
            entry.Name = String.IsNullOrEmpty(entry.Name) ? "[unknown]" : entry.Name;

            var duplicate = Entries.SingleOrDefault(o => o.TimeInSeconds == entry.TimeInSeconds && o.Name.Equals(entry.Name, StringComparison.InvariantCultureIgnoreCase));

            if (duplicate == null)
            {
                Entries.Add(entry);

                Entries = Entries.OrderBy(o => o.TimeInSeconds).ThenBy(o => o.CreatedUtc).ToList();
            }
            else
            {
                entry = duplicate;
            }

            return Entries.IndexOf(entry) + 1;
        }


        public static string TimeDescription(float timeInSeconds)
        {
            return timeInSeconds.ToString("00.00");
        }
    }

    public static class LadderEntryExtensions
    {
        public static string TimeDescription(this Ladder.Entry entry)
        {
            return Ladder.TimeDescription(entry.TimeInSeconds);
        }
    }

}
