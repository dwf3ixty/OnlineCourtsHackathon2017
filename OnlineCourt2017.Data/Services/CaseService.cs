using OnlineCourt2017.Data.Interfaces;
using OnlineCourt2017.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Services
{
    public class CaseService : BaseService
    {
        private IUserIdentity user;
        private SecurityService security;

        public CaseService(AppDbContext context, IUserIdentity user, SecurityService security) : base(context)
        {
            this.user = user;
            this.security = security;
        }

        public Offer GetOffer(int offerId)
        {
           return this.dbContext.Offers.Where(w => w.OfferId == offerId).FirstOrDefault();
        }

        public List<OfferComment> GetOfferComments(int offerId)
        {
            return this.dbContext.OfferComments.Where(w => w.OfferId == offerId).ToList<OfferComment>();
        }

        public Case GetCurrentCase()
        {
            var currentUser = security.GetUser(this.user.GetUserName);
            if (currentUser == null)
            {
                return new Case();
            }

            var foundCase = this.dbContext.Cases.Where(w =>
                     (w.Defendant != null && w.DefendantId == currentUser.PersonId) ||
                     (w.Claimant != null && w.ClaimantId == currentUser.PersonId) ||
                     (w.CaseWorker != null && w.CaseWorkerId == currentUser.PersonId)
                ).Take(1).FirstOrDefault();

            if (foundCase == null)
            {
                return new Case();
            }

            return foundCase;
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
