using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers
{
    public class AccountConroller : Controller 
        //class
    {
        public IActionResult Index()
        {
            return View(); //return
        }
        private readonly ILogger<AccountConroller> _logger;
    }
}
