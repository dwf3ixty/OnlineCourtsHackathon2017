using OnlineCourt2017.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourt2017.Models
{
    public class CaseDashboardView
    {
        public Case Case { get; set; }

        public decimal? YourLatestOffer
        {
            get
            {
                if (this.IsUserDefendant)
                {
                    return this.DefendantOffers != null && this.DefendantOffers.Any() ? 
                        this.DefendantOffers.OrderByDescending(o => o.Key).FirstOrDefault().Value : null;
                }
                else
                {
                    return this.ClaimantOffers != null && this.ClaimantOffers.Any() ?
                        this.ClaimantOffers.OrderByDescending(o => o.Key).FirstOrDefault().Value : null;
                }
            }
        }

        public decimal? TheirLatestOffer
        {
            get
            {
                if (!this.IsUserDefendant)
                {
                    return this.DefendantOffers != null && this.DefendantOffers.Any() ?
                        this.DefendantOffers.OrderByDescending(o => o.Key).FirstOrDefault().Value : null;
                }
                else
                {
                    return this.ClaimantOffers != null && this.DefendantOffers.Any() ?
                        this.ClaimantOffers.OrderByDescending(o => o.Key).FirstOrDefault().Value : null;
                }
            }
        }

        public bool? YourLatestOfferPositive
        {
            get
            {
                if (this.IsUserDefendant)
                {
                    return this.IsLatestOfferGreaterThanPrevious(this.DefendantOffers);
                }
                else
                {
                    return !this.IsLatestOfferGreaterThanPrevious(this.ClaimantOffers);
                }
            }
        }

        public bool? TheirLatestOfferPostive
        {
            get
            {
                if (!this.IsUserDefendant)
                {
                    return this.IsLatestOfferGreaterThanPrevious(this.DefendantOffers);
                }
                else
                {
                    return !this.IsLatestOfferGreaterThanPrevious(this.ClaimantOffers);
                }
            }
        }

        private bool? IsLatestOfferGreaterThanPrevious(IDictionary<DateTime, decimal?> offers)
        {
            if (offers != null && offers.Count > 1)
            {
                var latestOffer = offers.OrderByDescending(o => o.Key).First(o => o.Value != null);
                var secondLastestOffer = offers.OrderByDescending(o => o.Key).First(o => o.Key < latestOffer.Key && o.Value != null);

                return latestOffer.Value.Value == secondLastestOffer.Value.Value ? null : (bool?)(latestOffer.Value.Value > secondLastestOffer.Value.Value);
            }
            else
            {
                return null;
            }
        }

        public bool IsUserDefendant { get; set; }

        public IDictionary<DateTime, decimal?> ClaimantOffers { get; set; }

        public IDictionary<DateTime, decimal?> DefendantOffers { get; set; }
    }
}