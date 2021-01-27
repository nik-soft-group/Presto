using System.Collections.Generic;

namespace NiksoftCore.FormBuilder.Service
{
    public class ListItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string KeyValue { get; set; }
        public int SortId { get; set; }
        public bool Enabled { get; set; }
        public int ControlId { get; set; }

        public virtual FormControl FormControl { get; set; }
        public virtual ICollection<FormControl> Childs { get; set; }
    }
}
