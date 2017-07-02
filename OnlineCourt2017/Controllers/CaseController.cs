using OnlineCourt2017.Data.Services;
using OnlineCourt2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourt2017.Controllers
{
    public class CaseController : BaseController
    {
        private CaseService service;
        private SecurityService security;

        public CaseController(CaseService service, SecurityService security) : base()
        {
            this.service = service;
            this.security = security;
        }

        public ActionResult Chat()
        {
            var userCase = this.service.GetCurrentCase();
            return this.View(userCase);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VideoTutorial()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);
            var hasSeenPreferences = false;

            switch (type)
            {
                case "defendant":
                    hasSeenPreferences = userCase.DefendantSeenPreferences;

                    break;
                case "claimant":
                    hasSeenPreferences = userCase.ClaimantSeenPreferences;

                    break;
            }

            ViewBag.HasSeenPreferences = hasSeenPreferences;

            return View();
        }

        public ActionResult Questionnaire()
        {
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);
            var hasSeenPreferences = false;
            var caseChanged = false;

            switch (type)
            {
                case "defendant":
                    hasSeenPreferences = userCase.DefendantSeenPreferences;

                    if (!userCase.DefendantSeenIntroVideo)
                    {
                        userCase.DefendantSeenIntroVideo = true;
                        caseChanged = true;
                    }

                    break;
                case "claimant":
                    hasSeenPreferences = userCase.ClaimantSeenPreferences;

                    if (!userCase.ClaimantSeenIntroVideo)
                    {
                        userCase.ClaimantSeenIntroVideo = true;
                        caseChanged = true;
                    }

                    break;
            }

            if (caseChanged)
            {
                service.Save();
            }

            ViewBag.HasSeenPreferences = hasSeenPreferences;

            return View();
        }

        public ActionResult Preferences()
        {
            var pref = new Preferences();
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            switch (type)
            {
                case "defendant":

                    pref.Name = userCase.Defendant.Forename + " " + userCase.Defendant.Surname;

                    if (userCase.DefendantAllowVideoConference == true)
                    {
                        pref.VideoConference = true;
                    }

                    if (userCase.DefendantAllowEmail == true)
                    {
                        pref.Email = true;
                    }

                    if (userCase.DefendantAllowFaceToFace == true)
                    {
                        pref.FaceToFace = true;
                    }

                    if (userCase.DefendantAllowTelephone == true)
                    {
                        pref.Telephone = true;
                    }

                    break;
                case "claimant":
                    pref.Name = userCase.Claimant.Forename + " " + userCase.Claimant.Surname;

                    if (userCase.ClaimantAllowVideoConference == true)
                    {
                        pref.VideoConference = true;
                    }

                    if (userCase.ClaimantAllowEmail == true)
                    {
                        pref.Email = true;
                    }

                    if (userCase.ClaimantAllowFaceToFace == true)
                    {
                        pref.FaceToFace = true;
                    }

                    if (userCase.ClaimantAllowTelephone == true)
                    {
                        pref.Telephone = true;
                    }

                    break;
            }

            return View(pref);
        }

        [HttpPost]
        public ActionResult Preferences(string videoconference, string facetoface, string email, string telephone)
        {
            var pref = new Preferences();
            var userCase = this.service.GetCurrentCase();
            var user = this.security.GetUser(this.User.UserName);

            var type = userCase.GetPersonType(user.PersonId);

            switch (type)
            {
                case "defendant":
                    pref.Name = userCase.Claimant.Forename + " " + userCase.Claimant.Surname;

                    if (videoconference != null && videoconference == "on")
                    {
                        userCase.DefendantAllowVideoConference = true;
                        pref.VideoConference = true;
                    }
                    else
                    {
                        userCase.DefendantAllowVideoConference = false;
                    }

                    if (email != null && email == "on")
                    {
                        userCase.DefendantAllowEmail = true;
                        pref.Email = true;
                    }
                    else
                    {
                        userCase.DefendantAllowEmail = false;
                    }

                    if (facetoface != null && facetoface == "on")
                    {
                        userCase.DefendantAllowFaceToFace = true;
                        pref.FaceToFace = true;
                    }
                    else
                    {
                        userCase.DefendantAllowFaceToFace = false;
                    }

                    if (telephone != null && telephone == "on")
                    {
                        userCase.DefendantAllowTelephone = true;
                        pref.Telephone = true;
                    }
                    else
                    {
                        userCase.DefendantAllowTelephone = false;
                    }

                    break;
                case "claimant":
                    pref.Name = userCase.Defendant.Forename + " " + userCase.Defendant.Surname;

                    if (videoconference != null && videoconference == "on")
                    {
                        userCase.ClaimantAllowVideoConference = true;
                        pref.VideoConference = true;
                    }
                    else
                    {
                        userCase.ClaimantAllowVideoConference = false;
                    }

                    if (email != null && email == "on")
                    {
                        userCase.ClaimantAllowEmail = true;
                        pref.Email = true;
                    }
                    else
                    {
                        userCase.ClaimantAllowEmail = false;
                    }

                    if (facetoface != null && facetoface == "on")
                    {
                        userCase.ClaimantAllowFaceToFace = true;
                        pref.FaceToFace = true;
                    }
                    else
                    {
                        userCase.ClaimantAllowFaceToFace = false;
                    }

                    if (telephone != null && telephone == "on")
                    {
                        userCase.ClaimantAllowTelephone = true;
                        pref.Telephone = true;
                    }
                    else
                    {
                        userCase.ClaimantAllowTelephone = false;
                    }

                    break;
            }

            service.Save();

            return View(pref);
        }
    }
}