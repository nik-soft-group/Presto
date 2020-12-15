namespace NiksoftCore.SystemBase.Service
{
    public class PortalLanguage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public bool IsDefault { get; set; }
        public bool Enabled { get; set; }
        public int PortalId { get; set; }
    }
}
