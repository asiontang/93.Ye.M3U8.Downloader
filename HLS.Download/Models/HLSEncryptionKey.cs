namespace HLS.Download.Models
{
    public class HLSEncryptionKey
    {
        // Properties
        public string Method { get; private set; }
        public string Path { get; private set; }

        // Constructor
        public HLSEncryptionKey(string line)
        {
            string[] parts = line.Split(',');

            Method = parts[0].ToString().Split('=')[1];
            Path = parts[1].ToString().Split('=')[1];
        }
    }
}
