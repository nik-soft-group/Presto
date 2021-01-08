using System;

namespace NiksoftCore.ITCF.Service
{
    public class Introduction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string VideoFile { get; set; }
        public string SoundFile { get; set; }
        public int GroupId { get; set; }
        public int BusinessId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual IntroductionGroup Group { get; set; }
        public virtual Business Business { get; set; }
    }
}
