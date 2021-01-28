namespace NiksoftCore.FormBuilder.Service
{
    public class FormAnswer
    {
        public long Id { get; set; }
        public int FormId { get; set; }
        public int ControlId { get; set; }
        public int ListItemId { get; set; }
        public int? UserId { get; set; }
        public string AnswerList { get; set; }

        public virtual Form Form { get; set; }
        public virtual FormControl FormControl { get; set; }
    }
}
