using Microsoft.AspNetCore.Mvc;
using WebApplication16.Models;

namespace WebApplication16.Controllers
{
    public class PizzaController : Controller
    {
        static public List<Pizza> pizzadetails = new List<Pizza>() {

                new Pizza { PizzaId = 100,Type = "chicago", Price =250},
                new Pizza { PizzaId = 101,Type = "california",Price=300},
                new Pizza { PizzaId = 102,Type = "Pepperoni",Price=450}
            };
        static public List<orderconformation> orderdetails = new List<orderconformation>();


        public IActionResult Index()
        {
            return View(pizzadetails);

        }


        public IActionResult Cart(int id)
        {
            var found = (pizzadetails.Find(p => p.PizzaId == id));

            TempData["id"] = id;

            return View(found);

        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            orderconformation o = new orderconformation();
            var found = (pizzadetails.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = Convert.ToInt32(Request.Form["qty"]);
            o.TotalPrice = o.Price * o.Quantity;

            orderdetails.Add(o);

            return RedirectToAction("Checkout");

        }


        public IActionResult Checkout()
        {

            //var found = orderdetails.Find(p => p.OrderId == orderid);

            //Console.WriteLine(orderdetails); 
            return View(orderdetails);

        }



    }
}

