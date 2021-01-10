using Microsoft.AspNetCore.Http;
using System;

namespace NiksoftCore.ITCF.Service
{
    public class IntroductionRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public IFormFile Image { get; set; }
        public string VideoFile { get; set; }
        public IFormFile Video { get; set; }
        public string SoundFile { get; set; }
        public IFormFile Sound { get; set; }
        public string KeyValue { get; set; }
        public int BusinessId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
