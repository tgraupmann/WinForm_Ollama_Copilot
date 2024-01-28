using System;
using System.Collections.Generic;

namespace YoutubeTranscriptApi
{
    //https://github.com/jdepoix/youtube-transcript-api/blob/master/youtube_transcript_api/_cli.py

    class YouTubeTranscriptCli
    {
        public YouTubeTranscriptCli(params string[] args)
        {
            throw new NotImplementedException();
        }

        public string Run()
        {
            var parsed_args = parseArgs();
            throw new NotImplementedException();
        }

        internal object fetchTranscript(string[] parsed_args, Dictionary<string, string> proxies, string cookies, string video_id)
        {
            using (var youTubeTranscriptApi = new YouTubeTranscriptApi())
            {
                var transcript_list = youTubeTranscriptApi.ListTranscripts(video_id, proxies: proxies, cookies: cookies);
            }
            throw new NotImplementedException();
        }

        internal object parseArgs()
        {
            throw new NotImplementedException();
        }

        internal string sanitizeVideoIds(params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
