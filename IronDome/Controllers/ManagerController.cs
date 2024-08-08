using IronDome.DAL;
using IronDome.Models;
using IronDome.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IronDome.Controllers
{
	public class ManagerController : Controller
	{
		public IActionResult Index()
		{
			List<DefenceAmmunition> defences = Data.Get.DefenceAmmunitions.ToList();
			return View(defences);
		}
		public IActionResult AttackArea()
		{
			List<Threat> theats = Data.Get.Threats.Include(t => t.Org).Include(t => t.type).ToList();
			return View(theats);
		}

		public IActionResult updateDefenceAmmiunition(int dfid, int amount)
		{
			DefenceAmmunition? da = Data.Get.DefenceAmmunitions.Find(dfid);
			da.amount = amount;
			Data.Get.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult createThreat()
		{
			ThreatView tv = new ThreatView
			{
				Types = Data.Get.ThreatAmmunitions.ToList()
					.Select(ta => new SelectListItem { Value = ta.id.ToString(), Text = ta.name }).ToList(),
				TerrorOrgs = Data.Get.TerrorOrgs.ToList()
					.Select(ta => new SelectListItem { Value = ta.id.ToString(), Text = ta.name }).ToList(),
			};
			return View(tv);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult createThreat(ThreatView tv)
		{

			TerrorOrg? to = Data.Get.TerrorOrgs.Find(tv.TerrorOrgId);
			ThreatAmmunition? ta = Data.Get.ThreatAmmunitions.Find(tv.TypeId);

			if (to == null || ta == null)
			{
				return NotFound();
			}
			Threat t = new Threat
			{
				Org = to,
				type = ta
			};

			Data.Get.Threats.Add(t);
			Data.Get.SaveChanges();

			return RedirectToAction(nameof(AttackArea));
		}


		public IActionResult Launch(int id)
		{
			Threat? t = Data.Get.Threats.Find(id);
			if (t == null)
			{
				return NotFound();
			}

			t.status = Utils.ThreatStatus.Active;
			t.fired_at = DateTime.Now;
			Data.Get.SaveChanges();

			return RedirectToAction(nameof(AttackArea));
		}
	}
}
