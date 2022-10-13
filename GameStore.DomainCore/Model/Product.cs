﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameStore.DomainCore.Model
{
    public class Product
    {
        public Product()
        {
            this.Reviews = new HashSet<Review>();
        }
        [Required]
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
