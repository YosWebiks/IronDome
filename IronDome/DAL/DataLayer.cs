using IronDome.Models;
using Microsoft.EntityFrameworkCore;

namespace IronDome.DAL
{
	public class DataLayer : DbContext
	{
		public DataLayer(string cs) : base(GetOptions(cs))
		{
			Database.EnsureCreated();

			Sid();
		}

		public DbSet<DefenceAmmunition> DefenceAmmunitions { get; set; }
		public DbSet<TerrorOrg> TerrorOrgs { get; set; }
		public DbSet<Threat> Threats { get; set; }
		public DbSet<ThreatAmmunition> ThreatAmmunitions { get; set; }



		private void Sid()
		{
			if (!DefenceAmmunitions.Any())
			{
				DefenceAmmunition defence1 = new DefenceAmmunition { name = "Iron Dome Missle", amount = 100 };
				DefenceAmmunition defence2 = new DefenceAmmunition { name = "Patriot Missle", amount = 50 };
				DefenceAmmunitions.AddRange(defence1, defence2);
				SaveChanges();
			}

			if (!TerrorOrgs.Any())
			{
				TerrorOrgs.AddRange(
					new TerrorOrg { name = "Hamas", location = "Lebanon", distance = 70 },
					new TerrorOrg { name = "Hezbollah", location = "Lebanon", distance = 100 },
					new TerrorOrg { name = "Houthi", location = "Yamen", distance = 2377 },
					new TerrorOrg { name = "Iran", location = "Iran", distance = 1600 }
					);
				SaveChanges();
			}

			if (!ThreatAmmunitions.Any())
			{
				ThreatAmmunitions.AddRange(
					new ThreatAmmunition { name = "Balisti", speed = 18000 },
					new ThreatAmmunition { name = "Rocket", speed = 880 },
					new ThreatAmmunition { name = "Catbam", speed = 300 }
					);
				SaveChanges();
			}

			if (!Threats.Any())
			{
				TerrorOrg? hamas = TerrorOrgs.FirstOrDefault(t => t.name == "Hamas");
				ThreatAmmunition? rocket = ThreatAmmunitions.FirstOrDefault(t => t.name == "Rocket");

				if (hamas != null && rocket != null)
				{
					Threats.AddRange(
						new Threat
						{
							Org = hamas,
							type = rocket,
						}
						);
					SaveChanges();
				}

			}

		}


		private static DbContextOptions GetOptions(string cs)
		{
			return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), cs).Options;
		}
	}
}
