using Microsoft.AspNetCore.Http;
using System;

namespace NiksoftCore.ITCF.Service
{
    public class IntroGroupRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyValue { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool Enabled { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
