using LegacyApp.Enums;

namespace LegacyApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClientStatus ClientStatus { get; set; }
        public ClientType ClientType { 
            get {
                if (Name == "VeryImportantClient")
                {
                    return ClientType.VeryImportantClient;
                }
                if (Name == "ImportantClient")
                {
                    return ClientType.ImportantClient;
                }
                return ClientType.None;
            } 
            set { 
                ClientType = value; 
            } 
        }
    }
}