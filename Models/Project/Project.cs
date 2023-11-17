﻿using System.Data;

namespace BugTrackingSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int PersonalSpaceID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public PersonalSpace PersonalSpace { get; set; }
        public List<Bug> Bugs { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Role> CreatedRoles { get; set; }
    }
}
