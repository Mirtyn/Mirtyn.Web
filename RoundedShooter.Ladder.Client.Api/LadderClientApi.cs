﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;

namespace RoundedShooter
{
    public class LadderClientApi
    {
        public enum PostResponseStatus
        {
            Error = 0,
            Inserted = 1,
        }

        public class PostResponse
        {
            public PostResponseStatus Status { get; set; }

            public int Position { get; set; }

        }

        private string LadderPostUrl { get; set; }

        public LadderClientApi(string ladderPostUrl)
        {
            LadderPostUrl = ladderPostUrl;
        }

        public bool TryPost(Ladder.Entry entry, string version, out PostResponse response)
        {
            response = null;

            try
            {
                response = Post(entry, version);

                return true;
            }
            catch
            {

            }

            return false;
        }

        public PostResponse Post(Ladder.Entry entry, string version)
        {
            var postResponse = new PostResponse();

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(entry);

                var values = new Dictionary<string, string>
                    {
                       { "json", json },
                       { "version", version },
                    };

                var content = new FormUrlEncodedContent(values);

                using (var response = httpClient.PostAsync(LadderPostUrl, content).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                //using (var jsonTextReader = new JsonTextReader(streamReader))
                                //{
                                    postResponse = JsonConvert.DeserializeObject<PostResponse>(streamReader.ReadToEnd());

                                    //postResponse = new JsonSerializer().Deserialize<PostResponse>(jsonTextReader);
                                //}
                            }
                        }
                    }
                }
            };

            return postResponse;
        }
    }
}
