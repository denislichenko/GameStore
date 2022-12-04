using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameStore.DomainCore.Model
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string ConfirmationNumber { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
