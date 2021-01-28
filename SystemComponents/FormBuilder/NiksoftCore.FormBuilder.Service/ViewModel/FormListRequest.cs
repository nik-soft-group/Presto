using NiksoftCore.ViewModel;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormListRequest : BaseRequest
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
