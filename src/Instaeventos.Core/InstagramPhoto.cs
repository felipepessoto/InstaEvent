using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instaeventos.Core
{
    public class InstagramPhoto
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public string FullResponse { get; set; }
        public string InstagramUsername { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string IdInstagram { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
