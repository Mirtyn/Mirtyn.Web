using Newtonsoft.Json;
using ProjectBoost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mirtyn.Web
{
    public class ProjectBoostServerApi
    {
        private string StorePath;

        public ProjectBoostServerApi(string storePath)

        {
            StorePath = storePath;
        }

        //static LadderServerApi()
        //{
        //    StorePath = Project.MapPath("~/data/ladders/");
        //}

        public IOrderedEnumerable<Version> EnumerateSavedLadderVersions()
        {
            return Directory.EnumerateFiles(StorePath, "*.json").Select(o => Version.Parse(Path.GetFileNameWithoutExtension(o))).OrderByDescending(o => o);
        }

        public Ladder LoadLatest()
        {
            return Load(EnumerateSavedLadderVersions().FirstOrDefault());
        }

        public Ladder Load(string version)
        {
            return Load(VersionStringToVersion(version));
        }

        public Ladder Load(Version version)
        {
            if (version == null)
            {
                return null;
            }

            var path = Path.Combine(StorePath, FileName(version));

            if (!File.Exists(path))
            {
                return null;
            }

            var ladder = JsonConvert.DeserializeObject<Ladder>(File.ReadAllText(path));

            ConvertLadder(ladder);

            return ladder;
        }

        private void ConvertLadder(Ladder ladder)
        {
            if (ladder.Entries != null && ladder.Entries.Count > 0 && (ladder.FlagEntryDictionary == null || ladder.FlagEntryDictionary.Count == 0))
            {
                ladder.FlagEntryDictionary = new Dictionary<Ladder.LadderFlag, List<Ladder.Entry>>();

                foreach (var entry in ladder.Entries)
                {
                    ladder.AddUniqueEntry(entry);
                }

                ladder.Entries = null;
            }
        }

        public LadderClientApi.PostResponse Save(Ladder.Entry entry, string version)
        {
            return Save(entry, VersionStringToVersion(version));
        }

        public LadderClientApi.PostResponse Save(Ladder.Entry entry, Version version)
        {
            var ladder = Load(version);

            if (ladder == null)
            {
                ladder = new Ladder { Version = version };
            }
            else
            {
                ConvertLadder(ladder);
            }

            var response = new LadderClientApi.PostResponse
            {
                Position = ladder.AddUniqueEntry(entry),
                Status = LadderClientApi.PostResponseStatus.Inserted,
            };

            Save(ladder);

            return response;
        }

        private void Save(Ladder ladder)
        {
            var path = Path.Combine(StorePath, FileName(ladder.Version));

            File.WriteAllText(path, JsonConvert.SerializeObject(ladder, Formatting.Indented));
        }

        private static string FileName(Version version)
        {
            return version.ToString() + ".json";
        }

        public static Version VersionStringToVersion(string versionString)
        {
            if (Version.TryParse(StripNonNumeric(versionString), out Version version))
            {
                return version;
            }
            return new Version("0.0.0.0");
        }

        private static string StripNonNumeric(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());
        }
    }
}
