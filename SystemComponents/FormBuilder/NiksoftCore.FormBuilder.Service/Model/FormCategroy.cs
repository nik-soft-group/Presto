using System.Collections.Generic;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormCategroy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyValue { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string File { get; set; }
        public int? ParentId { get; set; }

        public virtual FormCategroy Parent { get; set; }
        public virtual ICollection<FormCategroy> Childs { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
    }
}
