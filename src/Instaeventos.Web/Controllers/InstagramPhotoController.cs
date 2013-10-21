using Instaeventos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Instaeventos.Web.Controllers
{
    public class InstagramPhotoController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<InstagramPhoto> Get(int idEvent, bool newPhotos=false)
        {
            var config = new InstaSharp.InstagramConfig("554dfe9286994bbe98417d8dc7b69a24", "39de8776637b47d2829cd1a4708ae180", "http://blog.fujiy.net", "http://blog.fujiy.net");

            using (InstaeventosContext context = new InstaeventosContext())
            {
                if (context.Events.Any(x => x.Id == idEvent && x.AutomaticApproval))
                {
                    new InstagramPhotoFetcher(context, config).ImportNewPhotos(idEvent);
                }

                IQueryable<InstagramPhoto> query = context.InstagramPhotos.Where(x =>x.Event.Id == idEvent && x.Approved);
                if (newPhotos)
                {
                    query = query.Where(x => x.NeverShown);
                }

                var photos = query.ToList();

                foreach (var item in photos)
                {
                    item.NeverShown = false;
                }
                context.SaveChanges();

                return photos;
            }
        }
    }
}