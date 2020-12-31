using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.ViewModel
{
    public class BusinessCategoryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile IconFile { get; set; }
        public string Icone { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
    }
}
