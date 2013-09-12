using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instaeventos.Web.ViewModels
{
    public class EventConfigureSave
    {
        public string Name { get; set; }
        public string HashTag { get; set; }
        public bool AutomaticApproval { get; set; }
    }
}