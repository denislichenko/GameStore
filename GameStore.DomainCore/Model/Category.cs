using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameStore.DomainCore.Model
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }
    }
}
