using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.msg = TempData["msg"];
            return View();
        }
        
        [HttpPost]
        [Route("quotes")]
        public IActionResult AddQuote(string  name = null, string quote = null)
        {
            if(name == null || quote == null){
                TempData["msg"] = "You can not add an empty field.";
                return RedirectToAction("Index");
            }
            string add = "INSERT INTO quotes(content, creator, createdAt, updatedAt) VALUES ('" + quote + "', '" + name + "', NOW(), NOW())";
            DbConnector.Execute(add);
            TempData["msg"] = "Success! You added a new quote!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult ShowAll()
        {
            string query = "SELECT * FROM quotes ORDER BY createdAt DESC";
            var allQuotes = DbConnector.Query(query);
            ViewBag.allQuotes = allQuotes;
            return View();
        }
    }
}
