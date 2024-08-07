using IronDome.Models;

namespace IronDome.ViewModel
{
    public class ThreatView
    {
        public IEnumerable<TerrorOrg> orgs { get; set; }
        public IEnumerable<ThreatAmmunition> types { get; set; }
        public IEnumerable<Threat> threats { get; set; }

    }
}
