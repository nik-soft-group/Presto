using Microsoft.AspNetCore.Http;

namespace NiksoftCore.FormBuilder.Service
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyValue { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string FileUrl { get; set; }
        public IFormFile File { get; set; }
        public int? ParentId { get; set; }
    }
}
