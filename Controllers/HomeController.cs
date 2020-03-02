using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Declare HttpClient
            HttpClient hClient = new HttpClient();
            //Get all accounts from API
            String result = hClient.GetStringAsync("https://frontiercodingtests.azurewebsites.net/api/accounts/getall").Result;
            //Convert string to list of Account objects
            List<Account> accounts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Account>>(result);
            //Create new sorted list
            List<Account> accountsSortedList = accounts.OrderBy(o => o.AccountStatusId).ToList();
            //Save list to ViewData
            ViewData["Accounts"] = accountsSortedList;
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
