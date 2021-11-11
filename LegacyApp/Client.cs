namespace LegacyApp
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClientStatus ClientStatus { get; set; }

        public bool IsVeryImportantClient()
        {
            return Name == "VeryImportantClient";
        }

        public bool IsImportantClient()
        {
            return Name == "ImportantClient";
        }
    }
}