using System.Collections.Generic;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormControl
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public int FormId { get; set; }
        public string Label { get; set; }
        public string KeyValue { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public ControlType ControlType { get; set; }
        public string ReferenceId { get; set; }
        public int? ListControlId { get; set; }

        public virtual Form Form { get; set; }
        public virtual ListControl ListControl { get; set; }
        public virtual ICollection<FormAnswer> FormAnswers { get; set; }
    }
}
