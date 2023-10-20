using GameStore.DomainCore.Infrastructure;
using GameStore.DomainCore.Model;
using GameStore.Web.Models.DTO;
using GameStore.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly GameStoreDBContext _context; 

        public CategoryController(GameStoreDBContext context)
        {
            _context = context;
        }

        public List<CategoryDTO> Get()
        {
            List<CategoryDTO> categories = _context.Categories
                .Select(c => new CategoryDTO { CategoryId = c.CategoryId, CategoryName = c.CategoryName })
                .ToList();
            return categories;
        }

        // GET api/<controller>/5
        public CategoryDTO Get(int id)
        {
            Category c = _context.Categories.Find(id);
            CategoryDTO category = new CategoryDTO { CategoryId = c.CategoryId, CategoryName = c.CategoryName };
            return category;
        }

        // GET: api/Category/GetCount/
        [Route("api/Category/GetCount")]
        public int GetCount()
        {
            List<CategoryDTO> categories = _context.Categories.Select(c => new CategoryDTO { CategoryId = c.CategoryId, CategoryName = c.CategoryName }).ToList();
            return categories.Count();
        }

        // POST api/<controller>
        [Route("api/Category/Create")]
        public IActionResult Create([FromBody] CategoryViewModel value)
        {
            bool exist = _context.Categories.Any(c => c.CategoryName.Equals(value.CategoryName, StringComparison.OrdinalIgnoreCase));
            if (exist)
            {
                return BadRequest();
            }
            Category category = new Category();
            category.CategoryName = value.CategoryName;
            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok();
        }

        //PUT REMOVED
        // Ajax.htmlForm does not support put and delete, only supports get and post.
        public IActionResult Post([FromBody] CategoryViewModel value)
        {
            if (value == null || String.IsNullOrEmpty(value.CategoryName))
            {
                return Ok("Category Name can't be empty!");
            }

            bool exist = _context.Categories.Any(c => c.CategoryId == value.CategoryId);
            if (!exist)
            {
                return Ok("Category [" + value.CategoryId + "] does not exist!");
            }

            exist = _context.Categories.Where(c => c.CategoryId != value.CategoryId).Any(c => c.CategoryName.Equals(value.CategoryName, StringComparison.OrdinalIgnoreCase));
            if (exist)
            {
                return Ok("Category [" + value.CategoryName + "] is already existed, please try another name!");
            }
            var category = _context.Categories.Find(value.CategoryId);
            category.CategoryName = value.CategoryName;
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<controller>/5
        public IActionResult Delete(int id)
        {
            bool exist = _context.Products.Any(p => p.CategoryId == id);
            if (exist)
            {
                return Ok("There are products belong to Category [" + id + "], delete them first!");
            }
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok();
        }
    }
}
