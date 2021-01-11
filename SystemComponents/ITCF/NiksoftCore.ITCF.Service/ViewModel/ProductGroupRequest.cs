using Microsoft.AspNetCore.Http;

namespace NiksoftCore.ITCF.Service
{
    public class ProductGroupRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public int BusinessId { get; set; }
    }
}
