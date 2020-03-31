using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<WeatherForecast> WF = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64591/");
                //HTTP GET
                var responseTask = client.GetAsync("WeatherForecast");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<WeatherForecast>>();
                    readTask.Wait();

                    WF = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    WF = Enumerable.Empty<WeatherForecast>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(WF);
        }
    }
}
