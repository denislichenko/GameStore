using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameStore.DomainCore.Identity
{
    public class AppUser : IdentityUser
    {
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    //// Add custom user claims here
        //    //return userIdentity;
        //}

        public virtual string Membership { get; set; }

        public ICollection<AppRole> UserRoles { get; set; }
    }

    public class UserMetaData
    {
        [Required(ErrorMessage = "Please enter a role name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
    }
}