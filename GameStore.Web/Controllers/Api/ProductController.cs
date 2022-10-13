using GameStore.DomainCore.Infrastructure;
using GameStore.DomainCore.Model;
using GameStore.Web.Models.DTO;
using GameStore.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
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
    public class ProductController : ControllerBase
    {
        private readonly GameStoreDBContext _context;

        public ProductController(GameStoreDBContext context)
        {
            _context = context;
        }

        public List<ProductDTO> Get(CategoryViewModel value)
        {
            if (value.CategoryId == 0)
            {
                var query = from product in _context.Products
                            join category in _context.Categories
                              on product.CategoryId equals category.CategoryId
                            select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };

                List<ProductDTO> products = query.ToList();
                return products;
            }
            else
            {
                var query = from product in _context.Products
                            where product.CategoryId == value.CategoryId
                            join category in _context.Categories
                              on product.CategoryId equals category.CategoryId
                            select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                List<ProductDTO> products = query.ToList();
                return products;
            }
        }

        //[Authorize(Roles = "Admin, Advanced")]
        [Route("api/Product/GetByUserId")]
        public List<ProductDTO> GetByUserId(CategoryViewModel value)
        {
            String userid = User.Identity.GetUserId();
            if (value.CategoryId == 0)
            {
                var query = from product in _context.Products
                            where product.UserId == userid
                            join category in _context.Categories
                              on product.CategoryId equals category.CategoryId
                            select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                List<ProductDTO> products = query.ToList();
                return products;
            }
            else
            {
                var query = from product in _context.Products
                            where product.CategoryId == value.CategoryId
                               && product.UserId == userid
                            join category in _context.Categories
                              on product.CategoryId equals category.CategoryId
                            select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                List<ProductDTO> products = query.ToList();
                return products;
            }
        }

        // GET api/<controller>/5
        public ProductDTO Get(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                ProductDTO productDTO = new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                return productDTO;
            }

        }

        // GET: api/Product/GetCount/
        [Route("api/Product/GetCount")]
        public int GetCount()
        {
            var query = from product in _context.Products
                        join category in _context.Categories
                          on product.CategoryId equals category.CategoryId
                        select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };

            List<ProductDTO> products = query.ToList();
            return products.Count();
        }

        [Route("api/Product/GetAutoComplete")]
        public List<String> GetAutoComplete(string name)
        {
            var query = from product in _context.Products
                        where product.ProductName.ToLower().Contains(name.ToLower())
                        select product.ProductName;

            List<String> names = query.ToList();
            return names;
        }

        //[Authorize(Roles = "Admin, Advanced")]
        [Route("api/Product/Create")]
        public IActionResult Create([FromBody] ProductViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.Discount < 0 || value.Discount > 100)
                {
                    return Ok("Discount must between 0 ~ 100.");
                }

                bool exist = _context.Products.Any(c => c.ProductName.Equals(value.ProductName, StringComparison.OrdinalIgnoreCase));
                if (exist)
                {
                    return Ok("Product [" + value.ProductName + "] is already existed, please try another name!");
                }

                Product newProduct = new Product();
                newProduct.ProductName = value.ProductName;
                newProduct.CategoryId = value.CategoryId;
                newProduct.Price = value.Price;
                newProduct.Image = value.Image;
                newProduct.Condition = value.Condition;
                newProduct.Discount = value.Discount;
                newProduct.UserId = User.Identity.GetUserId();

                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "Admin, Advanced")]
        public IActionResult Post([FromBody] ProductViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value == null || String.IsNullOrEmpty(value.ProductName))
                {
                    return Ok("Product Name can't be empty!");
                }

                if (value.Discount < 0 || value.Discount > 100)
                {
                    return Ok("Discount must between 0 ~ 100.");
                }

                bool exist = _context.Products.Any(c => c.ProductId == value.ProductId);
                if (!exist)
                {
                    return Ok("Product [" + value.ProductId + "] does not exist!");
                }

                exist = _context.Products.Where(c => c.ProductId != value.ProductId).Any(c => c.ProductName.Equals(value.ProductName, StringComparison.OrdinalIgnoreCase));
                if (exist)
                {
                    return Ok("Product [" + value.ProductName + "] is already existed, please try another name!");
                }
                var product = _context.Products.Find(value.ProductId);
                if (product == null)
                {
                    return Ok("No such product!");
                }

                product.ProductName = value.ProductName;
                product.CategoryId = value.CategoryId;
                product.Price = value.Price;
                product.Image = value.Image;
                product.Condition = value.Condition;
                product.Discount = value.Discount;
                _context.SaveChanges();
                //context.Entry(product).CurrentValues.SetValues(value);
                //context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //PUT REMOVED

        // DELETE api/<controller>/5
        //[Authorize(Roles = "Admin, Advanced")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return Ok("No such product");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}
