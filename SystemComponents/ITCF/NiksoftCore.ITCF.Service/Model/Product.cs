namespace NiksoftCore.ITCF.Service
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int GroupId { get; set; }
        public int BusinessId { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public virtual Business Business { get; set; }
    }
}
