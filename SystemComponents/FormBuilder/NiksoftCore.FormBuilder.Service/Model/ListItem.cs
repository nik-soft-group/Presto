using System.Collections.Generic;

namespace NiksoftCore.FormBuilder.Service
{
    public class ListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyValue { get; set; }
        public int OrderId { get; set; }
        public bool Enabled { get; set; }
        public int ListControlId { get; set; }

        public virtual ListControl ListControl { get; set; }
        public virtual ICollection<ListControl> Childs { get; set; }
    }
}
