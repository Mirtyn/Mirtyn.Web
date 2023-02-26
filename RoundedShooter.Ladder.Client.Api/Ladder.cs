using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RoundedShooter
{
    public class Ladder
    {
        [Flags]
        public enum EntryFlag
        {
            None = 0,
            //Competitive = 1 << 0,	// 1
            //RealLanding = 1 << 1,	// 2
            //OneLife = 1 << 2,	    // 4
        }

        [Flags]
        public enum LadderFlag
        {
            None = EntryFlag.None,
            //CompetitiveRealLanding = EntryFlag.Competitive | EntryFlag.RealLanding,
        }

        public Version Version { get; set; }

        public Dictionary<LadderFlag, List<Entry>> FlagEntryDictionary { get; set; }

        public Ladder()
        {
            FlagEntryDictionary = new Dictionary<LadderFlag, List<Entry>>();
        }

        public class Entry
        {
            public DateTime CreatedUtc { get; set; }

            public string Name { get; set; }

            public long Points { get; set; }

            public EntryFlag Flag { get; set; } = EntryFlag.None;

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
            return EntriesFor(ToLadderFlag(entry.Flag));
        }

        public List<Entry> EntriesFor(LadderFlag ladderFlag)
        {
            if (!FlagEntryDictionary.ContainsKey(ladderFlag))
            {
                return null;
            }

            return FlagEntryDictionary[ladderFlag];
        }

        private Ladder.LadderFlag ToLadderFlag(Ladder.EntryFlag flag)
        {
            var ladderFlag = Ladder.LadderFlag.None;

            if (flag.Has(Ladder.EntryFlag.None))
            {
                ladderFlag = Ladder.LadderFlag.None;
            }

            return ladderFlag;
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
            var ladderFlag = ToLadderFlag(entry.Flag);

            if(EntriesFor(ladderFlag) == null)
            {
                FlagEntryDictionary.Add(ladderFlag, new List<Entry>());
            }

            FlagEntryDictionary[ladderFlag].Add(entry);

            FlagEntryDictionary[ladderFlag] = FlagEntryDictionary[ladderFlag].OrderBy(o => o.Points).ThenBy(o => o.CreatedUtc).ToList();

            return FlagEntryDictionary[ladderFlag].IndexOf(entry);
        }

        public static string PointsDescription(long point)
        {
            return point.ToString("N", new CultureInfo("bl-BE"));
        }
    }

    public static class LadderEntryExtensions
    {
        public static string PointsDescription(this Ladder.Entry entry)
        {
            return Ladder.PointsDescription(entry.Points);
        }
    }

}
