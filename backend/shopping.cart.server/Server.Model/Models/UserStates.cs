﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System.Collections.Generic;

#nullable disable

namespace Server.Model.Models
{
    public partial class UserStates
    {
        public UserStates()
        {
            Users = new HashSet<Users>();
        }

        public int UserStateId { get; set; }
        public string UserState { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}