using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ProjectBoost
{
    public class Ladder
    {
        [Flags]
        public enum EntryFlag
        {
            Freeplay = 0,
            Competitive = 1 << 0,	// 1
            RealLanding = 1 << 1,	// 2
            OneLife = 1 << 2,	    // 4

            Hardcore = OneLife,

            CompetitiveHardcore = Competitive | Hardcore,
            CompetitiveRealLanding = Competitive | RealLanding,
            CompetitiveRealLandingHardcore = CompetitiveRealLanding | Hardcore,
        }

        [Flags]
        public enum WorldFlag
        {
            [Display(Name = "World 1")]
            World1,
            [Display(Name = "World 2")]
            World2,
            [Display(Name = "World 3")]
            World3,
            [Display(Name = "World 4")]
            World4,
            [Display(Name = "World 5")]
            World5,
            [Display(Name = "World 6")]
            World6,
        }

        [Obsolete("Use EntryFlag")]
        [Flags]
        public enum LadderFlag
        {
            [Obsolete("Use EntryFlag.Competitive")]
            Competitive = EntryFlag.Competitive,
            [Obsolete("Use EntryFlag.CompetitiveRealLanding")]
            CompetitiveRealLanding = EntryFlag.Competitive | EntryFlag.RealLanding,
        }

        public Version Version { get; set; }

        public List<Entry> Entries { get; set; }

        [Obsolete("Use Entries instead")]
        public Dictionary<LadderFlag, List<Entry>> FlagEntryDictionary { get; set; }

        public Ladder()
        {
            Entries = new List<Entry>();
            FlagEntryDictionary = new Dictionary<LadderFlag, List<Entry>>();
        }

        public class Entry
        {
            public DateTime CreatedUtc { get; set; }

            public string Name { get; set; }

            public float Time { get { return TimeInSeconds; } set { TimeInSeconds = value; } }

            public float TimeInSeconds { get; set; }

            public EntryFlag Flag { get; set; } = EntryFlag.Competitive;

            public WorldFlag WorldFlag { get; set; } = WorldFlag.World1;

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

        //public IEnumerable<Entry> EntriesFor(WorldFlag worldFlag, Entry entry)
        //{
        //    return EntriesFor(worldFlag, ToLadderFlag(entry.Flag));
        //}

        public IEnumerable<Entry> EntriesFor(Entry entry)
        {
            return EntriesFor(entry.WorldFlag, entry.Flag);
        }

        public IEnumerable<Entry> EntriesFor(WorldFlag worldFlag)
        {
            return Entries.Where(o => o.WorldFlag == worldFlag).OrderBy(o => o.TimeInSeconds).ThenBy(o => o.CreatedUtc);
        }

        public IEnumerable<Entry> EntriesFor(WorldFlag worldFlag, EntryFlag entryFlag)
        {
            return Entries.Where(o => o.WorldFlag == worldFlag && o.Flag == entryFlag).OrderBy(o => o.TimeInSeconds).ThenBy(o => o.CreatedUtc);
        }

        public IEnumerable<Entry> EntriesFor(WorldFlag worldFlag, IEnumerable<EntryFlag> entryFlags)
        {
            return Entries.Where(o => o.WorldFlag == worldFlag && entryFlags.Contains(o.Flag)).OrderBy(o => o.TimeInSeconds).ThenBy(o => o.CreatedUtc);
        }

        //private Ladder.LadderFlag ToLadderFlag(Ladder.EntryFlag flag)
        //{
        //    var ladderFlag = Ladder.LadderFlag.Competitive;

        //    if (flag.Has(Ladder.EntryFlag.RealLanding))
        //    {
        //        ladderFlag = Ladder.LadderFlag.CompetitiveRealLanding;
        //    }

        //    return ladderFlag;
        //}

        private Entry FindDuplicate(Entry entry, out int index)
        {
            index = -1;

            var entries = EntriesFor(entry).ToList();

            if(entries == null || entries.Count == 0)
            {
                return null;
            }

            var duplicate = entries.SingleOrDefault(o => o.WorldFlag == entry.WorldFlag && o.Flag.Has(entry.Flag) && o.TimeInSeconds == entry.TimeInSeconds && o.Name.Equals(entry.Name, StringComparison.InvariantCultureIgnoreCase));

            index = duplicate != null ? entries.IndexOf(duplicate) : -1;

            return duplicate;
        }

        private int AddEntry(Entry entry)
        {
            //var ladderFlag = ToLadderFlag(entry.Flag);

            //if(EntriesFor(entry.WorldFlag, ladderFlag) == null)
            //{
            //    FlagEntryDictionary.Add(ladderFlag, new List<Entry>());
            //}

            Entries.Add(entry);

            return Entries.Where(o => o.Flag == entry.Flag && o.WorldFlag == entry.WorldFlag).OrderBy(o => o.TimeInSeconds).ThenBy(o => o.CreatedUtc).ToList().IndexOf(entry);
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

        public static string WorldFlagDescription(this Ladder.Entry entry)
        {
            return entry.WorldFlag.GetAttribute<DisplayAttribute>().Name;
        }
    }

}
