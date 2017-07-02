using OnlineCourt2017.Data;
using OnlineCourt2017.Data.Interfaces;
using OnlineCourt2017.Data.Models;
using OnlineCourt2017.Data.Services;
using OnlineCourt2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourt2017.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private CaseService caseService;
        private SecurityService security;
        public HomeController(CaseService caseService, SecurityService security) : base()
        {
            this.caseService = caseService;
            this.security = security;
        }

        public ActionResult Index()
        {
            var userCase = this.caseService.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);
            var type = userCase.GetPersonType(user.PersonId);
            var hasCompletedQuestionnaire = true;

            switch (type)
            {
                case "defendant":
                    hasCompletedQuestionnaire = userCase.DefendantSeenIntroVideo;

                    break;
                case "claimant":
                    hasCompletedQuestionnaire = userCase.ClaimantSeenIntroVideo;

                    break;
            }

            if (!hasCompletedQuestionnaire)
            {
                return this.RedirectToAction("Questionnaire", "Case");
            }

            var caseViewModel = new CaseDashboardView()
            {
                Case = userCase,
                ClaimantOffers = new Dictionary<DateTime, decimal?>(),
                DefendantOffers = new Dictionary<DateTime, decimal?>(),
                IsUserDefendant = user.Person == userCase.Defendant
            };

            if (userCase != null && userCase.Offers != null && userCase.Offers.Any())
            {
                var offerDate = userCase.Offers.Min(c => c.Created).Date;

                if (offerDate <= DateTime.Now.Date)
                {
                    while (offerDate <= DateTime.Now)
                    {
                        var claimantOffer = userCase.Offers
                            .OrderByDescending(o => o.Created)
                            .FirstOrDefault(o => o.CreatedById == userCase.Claimant.PersonId && o.Created.Date == offerDate);

                        var defendantOffer = userCase.Offers
                            .OrderByDescending(o => o.Created)
                            .FirstOrDefault(o => o.CreatedById == userCase.Defendant.PersonId && o.Created.Date == offerDate);

                        caseViewModel.ClaimantOffers.Add(offerDate, claimantOffer?.Value);
                        caseViewModel.DefendantOffers.Add(offerDate, defendantOffer?.Value);

                        offerDate = offerDate.AddDays(1);
                    }
                }
            }

            return View(caseViewModel);
        }

        public ActionResult NoOffers()
        {
            var userCase = this.caseService.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var caseViewModel = new CaseDashboardView()
            {
                Case = userCase,
                ClaimantOffers = new Dictionary<DateTime, decimal?>(),
                DefendantOffers = new Dictionary<DateTime, decimal?>(),
                IsUserDefendant = user.Person == userCase.Defendant
            };

            return View(caseViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}