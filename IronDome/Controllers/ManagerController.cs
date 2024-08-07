using IronDome.DAL;
using IronDome.Models;
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

        public IActionResult updateDefenceAmmiunition(int dfid, int amount)
        {
            DefenceAmmunition? da = Data.Get.DefenceAmmunitions.Find(dfid);
            da.amount = amount;
            Data.Get.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
