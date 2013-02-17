using System;
using System.Web.Mvc;
using Glimpse.Mashups.Core.GitHub;

namespace Glimpse.Mashups.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contributors()
        {
            var contributorDataStore = new ContributorDataStoreInFile(AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\Contributors.bin");
            var contributors = contributorDataStore.GetAll();
            return View(contributors);
        }
    }
}