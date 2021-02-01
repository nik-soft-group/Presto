namespace NiksoftCore.FormBuilder.Service
{
    public class ItemRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyValue { get; set; }
        public int OrderId { get; set; }
        public bool Enabled { get; set; }
        public int ListControlId { get; set; }
    }
}
