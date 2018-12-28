namespace Speed_Server
{
    public class Configuration : XmlDeserializeConfigSectionHandler
    {
        public EnvironmentName EnvironmentName { get; set; }
        public Environment Environment { get; set; }
    }
}
