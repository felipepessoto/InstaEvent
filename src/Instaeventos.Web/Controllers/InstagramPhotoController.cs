using Instaeventos.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Instaeventos.Web.Controllers
{
    public class InstagramPhotoController : ApiController
    {
        // GET api/<controller>
        public async Task<IEnumerable<InstagramPhoto>> Get(int idEvent, bool newPhotos=false)
        {
            var config = new InstaSharp.InstagramConfig("554dfe9286994bbe98417d8dc7b69a24", "39de8776637b47d2829cd1a4708ae180");

            using (InstaeventosContext context = new InstaeventosContext())
            {
                if (context.Events.Any(x => x.Id == idEvent && x.AutomaticApproval))
                {
                    await new InstagramPhotoFetcher(context, config).ImportNewPhotos(idEvent);
                    context.SaveChanges();
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