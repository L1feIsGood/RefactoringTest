namespace LegacyApp
{
    public enum ClientStatus
    {
        Ok,
        Created,
        NotFound,
        InternalClientError
    }

    public class Client
    {
        public int Id { get; set; }
        public ClientType Type { get; set; }
        public ClientStatus ClientStatus { get; set; }
    }
}