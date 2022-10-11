using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DomainCore.Infrastructure;
using GameStore.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;

namespace GameStore.Web.Controllers
{
    public class MyOrderController : Controller
    {
        private readonly GameStoreDBContext _context;

        public MyOrderController(GameStoreDBContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            try
            {
                String userid = User.Identity.GetUserId();

                var orders = from o in _context.Orders
                             join u in _context.Users
                               on o.UserId equals u.Id
                             where o.UserId == userid
                             orderby o.OrderId descending
                             select new { o.OrderId, o.UserId, u.UserName, o.FullName, o.Address, o.City, o.State, o.Zip, o.ConfirmationNumber, o.DeliveryDate };
                list = orders.Select(o => new OrderViewModel { OrderId = o.OrderId, UserId = o.UserId, UserName = o.UserName, FullName = o.FullName, Address = o.Address, City = o.City, State = o.State, Zip = o.Zip, ConfirmationNumber = o.ConfirmationNumber, DeliveryDate = o.DeliveryDate }).ToList();

                foreach (OrderViewModel order in list)
                {
                    var orderitems = from i in _context.OrderItems
                                     join p in _context.Products
                                       on i.ProductId equals p.ProductId
                                     join c in _context.Categories
                                       on p.CategoryId equals c.CategoryId
                                     where i.OrderId == order.OrderId
                                     select new { i.OrderItemId, i.OrderId, i.ProductId, p.ProductName, p.CategoryId, c.CategoryName, p.Price, p.Image, p.Condition, p.Discount, i.Quantity };
                    order.Items = orderitems.Select(o => new OrderItemViewModel { OrderItemId = o.OrderItemId, OrderId = o.OrderId, ProductId = o.ProductId, ProductName = o.ProductName, CategoryId = o.CategoryId, CategoryName = o.CategoryName, Price = o.Price, Image = o.Image, Condition = o.Condition, Discount = o.Discount, Quantity = o.Quantity }).ToList();
                }
                HttpContext.Session.SetInt32("OrderCount", orders.Count());

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error Occurs:" + ex.Message;
            }

            return View(list);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CancelOrder(int id)
        {

            var order = _context.Orders.Find(id);
            if (order == null)
            {
                ViewBag.Message = string.Format("No such order [{0}] found.", id);
            }
            else
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                ViewBag.Message = string.Format("Order [{0}] has been deleted!", id);
            }

            return RedirectToAction("Index");
        }
    }
}
