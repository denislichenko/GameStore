using GameStore.DomainCore.Model;

namespace GameStore.Web.Models.DTO
{
    public class ProductDTO : Product
    {
        public string CategoryName { get; set; }

        public double GetDiscountedPrice()
        {
            return Price * (100 - Discount) / 100;
        }
    }
}