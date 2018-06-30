namespace HLS.Download.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class HLSStream
    {
        // Properties
        public string TargetDuration { get; private set; }
        public string AllowCache { get; private set; }
        public string PlaylistType { get; private set; }
        public HLSEncryptionKey Key { get; private set; }
        public string Version { get; private set; }
        public string MediaSequence { get; private set; }
        public HLSStreamPart[] Parts { get; private set; }

        /// <summary>
        /// Parse a HLSStream object from a string
        /// </summary>
        /// <param name="m3u8">The string</param>
        /// <returns>Returns a parsed HLSStream object</returns>
        private static HLSStream ParseFromString(string m3u8)
        {
            string[] lines = m3u8.Split('\n');
            HLSStream STREAM = new HLSStream();
            List<HLSStreamPart> PARTS = new List<HLSStreamPart>();

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.Contains("TARGETDURATION"))
                    STREAM.TargetDuration = line.Split(':')[1];

                if (line.Contains("ALLOW-CACHE"))
                    STREAM.AllowCache = line.Split(':')[1];

                if (line.Contains("PLAYLIST-TYPE"))
                    STREAM.PlaylistType = line.Split(':')[1];

                if (line.Contains("KEY"))
                    STREAM.Key = new HLSEncryptionKey(line.Split(':')[1]);

                if (line.Contains("VERSION"))
                    STREAM.Version = line.Split(':')[1];

                if (line.Contains("MEDIA-SEQUENCE"))
                    STREAM.MediaSequence = line.Split(':')[1];

                if (line.Contains("EXTINF"))
                {
                    PARTS.Add(new HLSStreamPart(line, lines[i + 1]));
                    i = i + 2;
                }
            }

            STREAM.Parts = PARTS.ToArray();

            return STREAM;
        }

        /// <summary>
        /// Opens a HLSStream object from a given file path
        /// </summary>
        /// <param name="path">File path string</param>
        /// <returns>Returns a parsed HLSStream object</returns>
        public async static Task<HLSStream> Open(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return ParseFromString(await reader.ReadToEndAsync());
            }
        }

        /// <summary>
        /// Parses a HLSStream object from a string
        /// </summary>
        /// <param name="text">The string</param>
        /// <returns>Returns a parsed HLSStream object</returns>
        public static HLSStream Parse(string text)
        {
            return ParseFromString(text);
        }
    }
}
