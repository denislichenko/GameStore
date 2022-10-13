using GameStore.DomainCore.Infrastructure;
using GameStore.Web.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly GameStoreDBContext _context;

        public OrderController(GameStoreDBContext context)
        {
            _context = context;
        }

        // GET api/<controller>
        public List<OrderDTO> Get()
        {
            List<OrderDTO> list = new List<OrderDTO>();

            var orders = from o in _context.Orders
                         join u in _context.Users
                           on o.UserId equals u.Id
                         orderby o.OrderId descending
                         select new { o.OrderId, o.UserId, u.UserName, o.FullName, o.Address, o.City, o.State, o.Zip, o.ConfirmationNumber, o.DeliveryDate };
            list = orders.Select(o => new OrderDTO { OrderId = o.OrderId, UserId = o.UserId, UserName = o.UserName, FullName = o.FullName, Address = o.Address, City = o.City, State = o.State, Zip = o.Zip, ConfirmationNumber = o.ConfirmationNumber, DeliveryDate = o.DeliveryDate }).ToList();

            return list;
        }
        [Route("api/Order/GetOrderItems/{id}")]
        public List<OrderItemDTO> GetOrderItems(int id)
        {
            List<OrderItemDTO> list = new List<OrderItemDTO>();

            var orderitems = from i in _context.OrderItems
                             join p in _context.Products
                             on i.ProductId equals p.ProductId
                             join c in _context.Categories
                             on p.CategoryId equals c.CategoryId
                             where i.OrderId == id
                             select new { i.OrderItemId, i.OrderId, i.ProductId, p.ProductName, p.CategoryId, c.CategoryName, p.Price, p.Image, p.Condition, p.Discount, i.Quantity };
            list = orderitems.Select(o => new OrderItemDTO { OrderItemId = o.OrderItemId, OrderId = o.OrderId, ProductId = o.ProductId, ProductName = o.ProductName, CategoryId = o.CategoryId, CategoryName = o.CategoryName, Price = o.Price, Image = o.Image, Condition = o.Condition, Discount = o.Discount, Quantity = o.Quantity }).ToList();

            return list;
        }

        // GET: api/Order/GetCount/
        [Route("api/Order/GetCount")]
        public int GetCount()
        {
            return _context.Orders.Count();
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok();
        }
    }
}
