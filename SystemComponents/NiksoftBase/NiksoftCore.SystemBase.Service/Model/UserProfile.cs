using System;

namespace NiksoftCore.SystemBase.Service
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserId { get; set; }
    }
}
