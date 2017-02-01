namespace KerbalParser
{
    public class ConfigDirectory
    {
        public string directory;

        public string urlRoot;

        public UrlDir.DirectoryType type;

        public ConfigDirectory()
        {
            this.urlRoot = string.Empty;
            this.directory = ".";
            this.type = UrlDir.DirectoryType.Parts;
        }

        public ConfigDirectory(string urlRoot, string directory, UrlDir.DirectoryType type)
        {
            this.urlRoot = urlRoot;
            this.directory = directory;
            this.type = type;
        }
    }
}
