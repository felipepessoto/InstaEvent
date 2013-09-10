using Instaeventos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instaeventos.Web.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            using (InstaeventosContext context = new InstaeventosContext())
            {
                if (context.Events.Any() == false)
                {
                    var newEevent = context.Events.Add(new Event()
                    {
                        User = context.Users.First(),
                        BackgroundUrl = "/Content/Images/1.jpg",
                        CreatedDate = DateTime.Now,
                        Enabled = true,
                        HashTag = "fujiy",
                        IsPublic = true,
                        Name = "Casamento do Michael",
                        StartDate = new DateTime(2013, 1, 1),
                        EndDate = new DateTime(2014, 1, 1),
                        SliderEffect = "normal"
                    });
                    context.SaveChanges();
                }
            }
            return View();
        }

        public ActionResult Configure()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Approve()
        {
            var config = new InstaSharp.InstagramConfig("https://api.instagram.com/v1",
                "https://api.instagram.com/oauth", "554dfe9286994bbe98417d8dc7b69a24",
                "39de8776637b47d2829cd1a4708ae180", "http://blog.fujiy.net");

            var postsTag = new InstaSharp.Endpoints.Tags.Unauthenticated(config).Recent("newyork");

            return View(postsTag);
        }
    }
}