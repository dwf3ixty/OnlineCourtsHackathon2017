using OnlineCourt2017.Data.Models;
using OnlineCourt2017.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourt2017.Controllers
{
    public class OfferController : BaseController
    {
        private CaseService service;
        private SecurityService security;

        public OfferController(CaseService service, SecurityService security) : base()
        {
            this.service = service;
            this.security = security;
        }

        public ActionResult Details()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            var latestOffer = userCase.GetLatestOffer();

            if (latestOffer.OfferId == 0)
            {
                return RedirectToAction("Create");
            }

            return View(latestOffer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            var latestOffer = userCase.GetLatestOffer();

            return View(latestOffer);
        }

        [HttpPost]
        public ActionResult Create(Offer offer)
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            offer.CaseId = userCase.CaseId;
            offer.Created = DateTime.Now;
            offer.CreatedById = user.PersonId;

            userCase.Offers.Add(offer);
            this.service.Save();

            return RedirectToAction("Details");
        }

        public ActionResult Accept()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            var latestOffer = userCase.GetLatestOffer();

            //if (!latestOffer.Accepted)
            //{
            //    return RedirectToAction("Details");
            //}

            return View(latestOffer);
        }

        public ActionResult Payment()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            var latestOffer = userCase.GetLatestOffer();

            return View(latestOffer);
        }


        public ActionResult Summary()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            var latestOffer = userCase.GetLatestOffer();

            return View(latestOffer);
        }

        [HttpPost]
        public ActionResult StoreComment(int offerId, string message)
        {
            var offer = this.service.GetOffer(offerId);
            var user = this.security.GetUser(this.User.UserName);
            var comment = new OfferComment();
            comment.Created = DateTime.Now;
            comment.CreatedById = user.PersonId;
            comment.Details = message;
            comment.OfferId = offerId;

            offer.OfferComments.Add(comment);
            this.service.Save();

            // Get the previous comments
            var comments = this.service.GetOfferComments(offerId);
            var objectList = comments.OrderBy(o => o.Created).Select(s => new { Created = s.Created.ToShortDateString(), Forename = s.CreatedBy.Forename, Surname = s.CreatedBy.Surname, Details = s.Details }).ToList();
            return Json(objectList);
        }
    }
}