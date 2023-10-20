﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DomainCore.Identity
{
    [MetadataType(typeof(RoleMetaData))]
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string description)
            : base()
        {
            this.Description = description;
        }

        public virtual string Description { get; set; }
    }
    
    public class RoleMetaData
    {
        [Required(ErrorMessage = "Please enter a role name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
    }
}