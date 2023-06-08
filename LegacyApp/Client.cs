namespace LegacyApp
{
    public class Client
    {
        public const string VeryImportantClientName = "VeryImportantClient";
        public const string ImportantClientName = "ImportantClient";

        public int Id { get; set; }
        public string Name { get; set; }
        public ClientStatus ClientStatus { get; set; }
    }
}