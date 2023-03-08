using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RoundedShooter
{
    public class Ladder
    {
        //[Flags]
        public enum Flag
        {
            None = 0,

            Easy = 1,
            Medium,
            Hard,
            Endless,
            Master,
        }

        public Version Version { get; set; }

        public Dictionary<Flag, List<Entry>> FlagEntryDictionary { get; set; }

        public Ladder()
        {
            FlagEntryDictionary = new Dictionary<Flag, List<Entry>>();
        }

        public class Entry
        {
            public DateTime CreatedUtc { get; set; }

            public string Name { get; set; }

            public float TimeInSeconds { get; set; }

            public long Points { get; set; }

            public Flag Flag { get; set; } = Flag.None;

            public Entry()
            {
                CreatedUtc = DateTime.UtcNow;
            }
        }

        public int AddUniqueEntry(Entry entry)
        {
            entry.Name = String.IsNullOrEmpty(entry.Name) ? "[unknown]" : entry.Name;

            int index;

            var duplicate = FindDuplicate(entry, out index);

            if (duplicate == null)
            {
                index = AddEntry(entry);
            }

            return index + 1;
        }

        public List<Entry> EntriesFor(Entry entry)
        {
            return EntriesFor(entry.Flag);
        }

        public List<Entry> EntriesFor(Flag ladderFlag)
        {
            if (!FlagEntryDictionary.ContainsKey(ladderFlag))
            {
                return null;
            }

            return FlagEntryDictionary[ladderFlag];
        }

        private Entry FindDuplicate(Entry entry, out int index)
        {
            index = -1;

            var entries = EntriesFor(entry);

            if(entries == null || entries.Count == 0)
            {
                return null;
            }

            var duplicate = entries.SingleOrDefault(o => o.Points == entry.Points && o.Name.Equals(entry.Name, StringComparison.InvariantCultureIgnoreCase));

            index = duplicate != null ? entries.IndexOf(duplicate) : -1;

            return duplicate;
        }

        private int AddEntry(Entry entry)
        {
            if(EntriesFor(entry.Flag) == null)
            {
                FlagEntryDictionary.Add(entry.Flag, new List<Entry>());
            }

            FlagEntryDictionary[entry.Flag].Add(entry);

            FlagEntryDictionary[entry.Flag] = FlagEntryDictionary[entry.Flag].OrderByDescending(o => o.Points).ThenBy(o => o.CreatedUtc).ToList();

            return FlagEntryDictionary[entry.Flag].IndexOf(entry);
        }

        private static IFormatProvider PointsDescriptionFormatProvider = new CultureInfo("fr-FR");

        public static string PointsDescription(long point)
        {
            return point.ToString("N0", PointsDescriptionFormatProvider);
        }

        public static string TimeDescription(float timeInSeconds)
        {
            return timeInSeconds.ToString("00.00");
        }
    }

    public static class RoundedShooterLadderEntryExtensions
    {
        public static string TimeDescription(this RoundedShooter.Ladder.Entry entry)
        {
            return RoundedShooter.Ladder.TimeDescription(entry.TimeInSeconds);
        }

        public static string PointsDescription(this RoundedShooter.Ladder.Entry entry)
        {
            return RoundedShooter.Ladder.PointsDescription(entry.Points);
        }
    }

}
