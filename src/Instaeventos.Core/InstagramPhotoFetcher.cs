using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;

namespace Instaeventos.Core
{
    public class InstagramPhotoFetcher
    {
        private readonly InstagramConfig instagramConfig;
        private readonly InstaeventosContext context;

        public InstagramPhotoFetcher(InstaeventosContext context, InstagramConfig instagramConfig)
        {
            this.instagramConfig = instagramConfig;
            this.context = context;
        }

        public async Task ImportNewPhotos(int idEvent)
        {
            var currentEvent = context.Events.Find(idEvent);

            var postsTag = await new InstaSharp.Endpoints.Tags(instagramConfig).Recent(currentEvent.HashTag, currentEvent.NextMinTagId);

            foreach (var item in postsTag.Data)
            {
                context.InstagramPhotos.Add(new InstagramPhoto
                {
                    Event = currentEvent,
                    FullResponse = item.ToString(),
                    InstagramUsername = item.User.Username,
                    ImageUrl = item.Images.StandardResolution.Url,
                    PublishDate = item.CreatedTime,
                    IdInstagram = item.Id,
                    Description = item.Caption!=null?item.Caption.Text:"",
                    PostUrl = item.Link,
                    Approved = currentEvent.AutomaticApproval,
                    NeverShown = true,
                    CreatedDate = DateTime.Now
                });
            }
            currentEvent.NextMinTagId = postsTag.Pagination.NextMinId;
        }
    }
}
