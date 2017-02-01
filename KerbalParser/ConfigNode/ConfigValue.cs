namespace KerbalParser
{
    public class ConfigValue
    {
        public string name;

        public string value;

        public string comment;

        public ConfigValue(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public ConfigValue(string name, string value, string comment)
        {
            this.name = name;
            this.value = value;
            this.comment = comment;
        }

        public override string ToString()
        {
            return this.name + " = " + this.value;
        }
    }
}
