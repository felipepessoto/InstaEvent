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
        public IEnumerable<InstagramPhoto> Get(bool newPhotos=false)
        {
            using (InstaeventosContext context = new InstaeventosContext())
            {
                IQueryable<InstagramPhoto> query = context.InstagramPhotos.Where(x => x.Approved);
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