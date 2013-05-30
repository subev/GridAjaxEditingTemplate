using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoMVCWrappers.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace KendoMVCWrappers.Controllers
{
    public class HomeController : Controller
    {
        public static List<Person> people = new List<Person>();

        static HomeController()
        {
            people.Add(new Person { PersonID = 1, Name = "John", BirthDate = new DateTime(1968, 6, 26) });
            people.Add(new Person { PersonID = 2, Name = "Sara", BirthDate = new DateTime(1974, 9, 13) });
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult GetPeople([DataSourceRequest] DataSourceRequest dsRequest)
        {
            var result = people.ToDataSourceResult(dsRequest);
            return Json(result);
        }

        public ActionResult UpdatePerson([DataSourceRequest] DataSourceRequest dsRequest, [Bind(Prefix="models")]List<Person> updated)
        {
            if (people != null && ModelState.IsValid)
            {
                foreach (var person in updated)
                {
                    var toUpdate = people.FirstOrDefault(p => p.PersonID == person.PersonID);
                    toUpdate.Name = person.Name;
                    toUpdate.BirthDate = person.BirthDate;
                }
            }

            return Json(new Person[] { person }.ToDataSourceResult(dsRequest, ModelState));
        }

        public ActionResult CreatePerson([DataSourceRequest] DataSourceRequest dsRequest, [Bind(Prefix = "models")]List<Person> created)
        {
            if (people != null && ModelState.IsValid)
            {
                foreach (var person in created)
                {
                    person.PersonID = people.Count + 1;
                    people.Add(person);
                }
            }

            return Json(created.ToDataSourceResult(dsRequest, ModelState));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}
