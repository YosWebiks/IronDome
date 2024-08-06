using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronDome.Models
{
	public class Threat
	{
		[Key]
		public int id { get; set; }

		[NotMapped]
		public int responseTime { get; set; }

		public TerrorOrg Org { get; set; }

		public ThreatAmmunition type { get; set; }

		public bool isActive { get; set; }

		public DateTime? fired_at { get; set; }
	}
}
