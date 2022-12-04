using System.Collections.Generic;
using GameStore.DomainCore.Model;

namespace GameStore.Web.Models.DTO
{
    public class ProductOrderDTO : Product
    {
        public string CategoryName { get; set; }

        public double GetDiscountedPrice()
        {
            return Price * (100 - Discount) / 100;
        }

        public IEnumerable<OrderDTO> Orders { get; set; }
    }
}