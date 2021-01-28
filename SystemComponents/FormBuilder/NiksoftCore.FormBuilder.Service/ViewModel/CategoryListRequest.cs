using NiksoftCore.ViewModel;

namespace NiksoftCore.FormBuilder.Service
{
    public class CategoryListRequest : BaseRequest
    {
        public string Title { get; set; }
        public int ParentId { get; set; }
    }
}
