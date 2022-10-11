using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GameStore.DomainCore.Infrastructure;
using GameStore.DomainCore.Model;
using GameStore.Web.Models;
using GameStore.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly GameStoreDBContext _context;

        public ShoppingCartController(GameStoreDBContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            using MemoryStream ms = new MemoryStream(HttpContext.Session.Get("ShoppingCart"));
            BinaryFormatter bf = new BinaryFormatter();

            ShoppingCart cart = (ShoppingCart)bf.Deserialize(ms);
            if (cart == null)
            {
                cart = new ShoppingCart();

                using MemoryStream ms2 = new MemoryStream();
                bf.Serialize(ms2, cart);
                HttpContext.Session.Set("ShoppingCart", ms2.ToArray());
            }
            else
            {

                cart = (ShoppingCart)bf.Deserialize(ms);
            }
            return View(cart);
        }

        public ActionResult CreateOrUpdate(CartViewModel value)
        {
            ShoppingCart cart = new ShoppingCart(); //= //(ShoppingCart)Session["ShoppingCart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
                // Session["ShoppingCart"] = cart;
            }

            Product product = _context.Products.Find(value.Id);
            if (product != null)
            {
                if (value.Quantity == 0)
                {
                    cart.AddItem(value.Id, product);
                }
                else
                {
                    cart.SetItemQuantity(value.Id, value.Quantity, product);
                }
            }

            //Session["CartCount"] = cart.GetItems().Count();
            return View("Index", cart);
        }

        public ActionResult Checkout()
        {
            CheckoutViewModel checkout = new CheckoutViewModel();
            checkout.FullName = "Rong Zhuang";
            checkout.Address = "1st Jackson Ave,Chicago,IL";
            checkout.City = "Chicago";
            checkout.State = "IL";
            checkout.Zip = "60606";
            ViewBag.States = State.List();
            return View(checkout);
        }

        public ActionResult PlaceOrder(CheckoutViewModel value)
        {
            //ShoppingCart cart = (ShoppingCart)Session["ShoppingCart"];
            //if (cart == null)
            //{
            //    ViewBag.Message = "Your cart is empty!";
            //    return View("Index", "ShoppingCart");
            //}

            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Message = "Please provide valid shipping address!";
            //    return View("Checkout", "ShoppingCart");
            //}

            //Session["Checkout"] = value;

            //Create the URL and  concatenate  the Query String values
            String url = "http://ectweb2.cs.depaul.edu/ECTCreditGateway/Authorize.aspx";

            return Redirect(url);
        }
    }
}
