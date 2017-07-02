using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Models
{
    public class Case
    {
        public int CaseId { get; set; }

        public string CaseNumber { get; set; }

        public DateTime Created { get; set; }

        public int ClaimantId { get; set; }

        public virtual Person Claimant { get; set; }

        public int DefendantId { get; set; }

        public virtual Person Defendant { get; set; }

        public int CaseWorkerId { get; set; }

        public virtual Person CaseWorker { get; set; }

        public decimal ClaimValue { get; set; }

        public virtual IList<Offer> Offers { get; set; }

        public bool ClaimantSeenIntroVideo { get; set; }
        public bool ClaimantSeenPreferences { get; set; }

        public bool DefendantSeenIntroVideo { get; set; }
        public bool DefendantSeenPreferences { get; set; }

        public bool ClaimantAllowTelephone { get; set; }
        public bool ClaimantAllowFaceToFace { get; set; }
        public bool ClaimantAllowEmail { get; set; }
        public bool ClaimantAllowVideoConference { get; set; }

        public bool DefendantAllowTelephone { get; set; }
        public bool DefendantAllowFaceToFace { get; set; }
        public bool DefendantAllowEmail { get; set; }
        public bool DefendantAllowVideoConference { get; set; }

        public string Title { get; set; }

        public string GetPersonType(int personId)
        {
            if (this.DefendantId == personId)
            {
                return "defendant";
            }
            else if (this.ClaimantId == personId)
            {
                return "claimant";
            }
            else if (this.CaseWorkerId == personId)
            {
                return "caseworker";
            }

            return "unknown";
        }


        public Offer GetLatestOffer()
        {
            if(this.Offers != null && this.Offers.Count > 0)
            {
                return this.Offers.OrderByDescending(o => o.Created).FirstOrDefault();
            }

            return new Offer()
            {
                CaseId = this.CaseId,
                Case = this
            };
        }
    }
}
