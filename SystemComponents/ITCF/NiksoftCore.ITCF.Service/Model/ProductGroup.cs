using System.Collections.Generic;

namespace NiksoftCore.ITCF.Service
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BusinessId { get; set; }
        public virtual Business Business { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
