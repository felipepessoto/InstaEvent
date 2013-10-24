using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;
using InstaSharp.Models;

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

            List<Media> fotos = postsTag.Data;
            string nextMaxId = postsTag.Pagination.NextMaxId;
            string nextMinId = postsTag.Pagination.NextMinId;

            while (nextMaxId != null)
            {
                var postsTagPaginado = await new InstaSharp.Endpoints.Tags(instagramConfig).Recent(currentEvent.HashTag, null, nextMaxId);
                fotos.AddRange(postsTagPaginado.Data);
                nextMaxId = postsTagPaginado.Pagination.NextMaxId;
            }

            var idsRetorno = fotos.Select(x => x.Id).ToArray();
            var idsInstagramExistentes = context.InstagramPhotos.Where(x => idsRetorno.Contains(x.IdInstagram)).Select(x => x.IdInstagram).ToArray();

            foreach (var item in fotos.Where(x => idsInstagramExistentes.Contains(x.Id) == false))
            {
                context.InstagramPhotos.Add(new InstagramPhoto
                {
                    Event = currentEvent,
                    FullResponse = item.ToString(),
                    InstagramUsername = item.User.Username,
                    ImageUrl = item.Images.StandardResolution.Url,
                    PublishDate = item.CreatedTime,
                    IdInstagram = item.Id,
                    Description = item.Caption != null ? item.Caption.Text : "",
                    PostUrl = item.Link,
                    Approved = currentEvent.AutomaticApproval,
                    NeverShown = true,
                    CreatedDate = DateTime.Now
                });
            }

            if (nextMinId != null)
            {
                try
                {
                    if (long.Parse(nextMinId) > long.Parse(currentEvent.NextMinTagId ?? "0"))
                    {
                        currentEvent.NextMinTagId = nextMinId;
                    }
                }
                catch
                {
                    currentEvent.NextMinTagId = nextMinId;
                }
            }
        }
    }
}
