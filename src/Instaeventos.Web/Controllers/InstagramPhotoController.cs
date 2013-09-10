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
                IQueryable<InstagramPhoto> query = context.InstagramPhotos;
                if (newPhotos)
                {
                    //query = query.Where(x=>x.)
                }

                return query.ToList();
            }
        }
    }
}