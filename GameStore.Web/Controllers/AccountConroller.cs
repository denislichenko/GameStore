using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers
{
    public class AccountConroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
