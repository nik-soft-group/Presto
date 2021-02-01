using Microsoft.AspNetCore.Http;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyValue { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Template { get; set; }
        public bool IsPublic { get; set; }
        public bool Enabled { get; set; }
        public bool IsSavedIP { get; set; }
        public bool IsShare { get; set; }
        public string FileUrl { get; set; }
        public IFormFile FormFile { get; set; }
        public string ReferenceId { get; set; }
        public int CategoryId { get; set; }
    }
}
