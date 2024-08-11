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
		public static Dictionary<string, CancellationTokenSource> ThreatMap = new();
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

			// create cancelation token
			CancellationTokenSource cts = new();
			// create & run task
			Task task = Task.Run(async () =>
			{
				// print status every 2 seconds
				int timer = t.responseTime;
				while (!cts.IsCancellationRequested && timer > 0)
				{
					Console.WriteLine($"{t.id} threat is {timer} seconds away");
					await Task.Delay(2000);
					timer -= 2;
				}
				if (cts.IsCancellationRequested)
				{
					t.status = Utils.ThreatStatus.Failed;
				}
				else
				{
					t.status = Utils.ThreatStatus.Succeeded;
					cts.Cancel();
				}
				ThreatMap.Remove(t.id.ToString());
				Data.Get.SaveChanges();
			}, cts.Token);

			// save the threat in the dictionary
			ThreatMap[t.id.ToString()] = cts;

			return RedirectToAction(nameof(AttackArea));
		}

		public IActionResult DeffenceArea()
		{
			return View(Data.Get.Threats
				.Include(t => t.type)
				.Include(t => t.Org)
				.ToList()
				.Where(t => t.status != Utils.ThreatStatus.Inactive));
		}

		public IActionResult Intercept(int tid, int did)
		{
			// find threat
			Threat? t = Data.Get.Threats.Find(tid);
			// find deffence
			DefenceAmmunition? da = Data.Get.DefenceAmmunitions.Find(did);
			// make sure not null
			if (t == null || da == null)
			{
				return NotFound();
			}
			if (da.amount < 1)
			{
				return BadRequest($"{da.name} is out of ammunition! Please refill");
			}
			// cancel task and delete for dictionary
			ThreatMap[tid.ToString()].Cancel();

			// decrease the {da} amount and update the status of the threat
			--da.amount;

			Data.Get.SaveChanges();
			Thread.Sleep(500);
			return RedirectToAction(nameof(DeffenceArea));
		}
	}
}
