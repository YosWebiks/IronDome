using IronDome.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace IronDome.Models
{
	public class Threat
	{

		public Threat()
		{
			status = ThreatStatus.Inactive;
		}


		[Key]
		public int id { get; set; }

		[NotMapped]
		public int responseTime
		{
			get
			{
				return (int)(((double)Org.distance / (double)type.speed) * 3600);
			}
		}

		public TerrorOrg Org { get; set; }

		public ThreatAmmunition type { get; set; }

		public ThreatStatus status { get; set; } // inActive / active / failed / succeeded

		public DateTime? fired_at { get; set; }

	}
}
