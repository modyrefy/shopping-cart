using System;

namespace Server.Model.Dto.User
{
    public class ActiveUserContext
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
       // public string Password { get; set; }
        public string ParticipantName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public int UserRoleId { get; set; }
        public int UserStateId { get; set; }
        public string UserRole { get; set; }
        public string UserState { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
