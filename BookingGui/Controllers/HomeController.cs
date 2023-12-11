using BookingGui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HotelApi;

namespace BookingGui.Controllers
{
    //does not run in docker for some reason, however if you run HotelApi in docker, you can connect it. 
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8084/Hotel");
            var result = client.GetAsync(client.BaseAddress).Result.Content.ReadAsStringAsync().Result;

            if (result != null)
            {

                List<String> list = new List<String>();
                list.Add(result);
                ViewBag.AvailableHotels = list;
            }
            else
            {
                ViewBag.AvailableHotels = "no hotels available";
            }
            return View();
        }

        //this is how to make a new view (page), use it later if you want
       /* public IActionResult Privacy()
        {
            return View();
        }*/
    }
}