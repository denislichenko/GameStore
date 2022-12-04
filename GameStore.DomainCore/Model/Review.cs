using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GameStore.DomainCore.Identity;

namespace GameStore.DomainCore.Model
{
    public class Review
    {
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comments { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser User { get; set; }
    }
}
