using NiksoftCore.ViewModel;

namespace NiksoftCore.FormBuilder.Service
{
    public class ListItemRequest : BaseRequest
    {
        public string Title { get; set; }
        public int ControlId { get; set; }
    }
}
