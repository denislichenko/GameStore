using System;
using System.Collections.Generic;

namespace GameStore.Web.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ConfirmationNumber { get; set; }
        public DateTime DeliveryDate { get; set; }        
        public List<OrderItemViewModel> Items { get; set; }
    }
}