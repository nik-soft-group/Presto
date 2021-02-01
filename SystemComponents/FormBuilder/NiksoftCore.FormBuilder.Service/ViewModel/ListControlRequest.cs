namespace NiksoftCore.FormBuilder.Service
{
    public class ListControlRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
