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
        public ActionResult Index(int id)
        {
            using (InstaeventosContext context = new InstaeventosContext())
            {
                return View(context.InstagramPhotos.Where(x => x.Approved).ToList());
            }
        }

        public ActionResult Configure()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Approve(int id)
        {
            var config = new InstaSharp.InstagramConfig("https://api.instagram.com/v1",
                "https://api.instagram.com/oauth", "554dfe9286994bbe98417d8dc7b69a24",
                "39de8776637b47d2829cd1a4708ae180", "http://blog.fujiy.net");

            using (InstaeventosContext context = new InstaeventosContext())
            {
                var currentEvent = context.Events.Find(id);

                if (currentEvent == null)
                {
                    currentEvent = context.Events.Add(new Event()
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
                }

                var postsTag = new InstaSharp.Endpoints.Tags.Unauthenticated(config).Recent("csharp", null, currentEvent.NextMaxTagId);

                foreach (var item in postsTag.Data)
                {
                    break;
                    context.InstagramPhotos.Add(new InstagramPhoto()
                    {
                        Event = currentEvent,
                        FullResponse = item.ToString(),
                        InstagramUsername = item.User.Username,
                        ImageUrl = item.Images.StandardResolution.Url,
                        PublishDate = item.CreatedTime,
                        IdInstagram = item.Id,
                        Description = item.Caption,
                        PostUrl = item.Link,
                        Approved = currentEvent.AutomaticApproval,
                        CreatedDate = DateTime.Now
                    });
                }

                currentEvent.NextMaxTagId = postsTag.Pagination.NextMaxId;

                context.SaveChanges();

                return View(context.InstagramPhotos.Where(x => x.Approved == false).ToList());
            }
        }

        public ActionResult Approve(int id, int[] selectedItems)
        {
            using (InstaeventosContext context = new InstaeventosContext())
            {
                foreach (var item in context.InstagramPhotos.Where(x => selectedItems.Contains(x.Id)).ToList())
                {
                    item.Approved = true;
                }
                context.SaveChanges();
            }

            return RedirectToAction("Approve");
        }
    }
}