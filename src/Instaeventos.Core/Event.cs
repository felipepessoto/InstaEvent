using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instaeventos.Core
{
    public class Event
    {
        public  int Id { get; set; }
        public ApplicationUser User { get; set; }
        public  string Name { get; set; }
        public string HashTag { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Enabled { get; set; }
        public string BackgroundUrl { get; set; }
        public string SliderEffect { get; set; }
        public bool AutomaticApproval { set; get; }
        public bool IsPublic { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
