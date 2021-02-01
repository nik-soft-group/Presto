using System.Collections.Generic;

namespace NiksoftCore.FormBuilder.Service
{
    public class ListControl
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }

        public virtual ListItem Parent { get; set; }
        public virtual ICollection<ListItem> ListItems { get; set; }
        public virtual ICollection<FormControl> FormControls { get; set; }
    }
}
