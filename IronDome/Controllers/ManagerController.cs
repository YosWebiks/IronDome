using IronDome.DAL;
using IronDome.Models;
using IronDome.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
            List<Threat> theats = Data.Get.Threats.ToList();
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
            return View(new ThreatView
            {
                threats = Data.Get.Threats.ToList(),
                orgs = Data.Get.TerrorOrgs.ToList(),
                types = Data.Get.ThreatAmmunitions
            });
        }
    }
}
