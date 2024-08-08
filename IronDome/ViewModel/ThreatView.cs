using IronDome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IronDome.ViewModel
{
    public class ThreatView
    {
        public List<SelectListItem> TerrorOrgs { get; set; }
        public List<SelectListItem> Types { get; set; }

        public int TerrorOrgId { get; set; }
        public int TypeId { get; set; }



    }
}
