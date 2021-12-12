﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Model.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
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
        public DateTime CreatedDate { get; set; }

        public virtual Countries Country { get; set; }
        public virtual UserRoles UserRole { get; set; }
        public virtual UserStates UserState { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}