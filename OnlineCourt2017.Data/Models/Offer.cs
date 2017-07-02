using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        public int CaseId { get; set; }
        public virtual Case Case { get; set; }

        public DateTime Created { get; set; }

        public int? CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Person CreatedBy { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public bool Accepted { get; set; }
        
        public int? CounterOfferId { get; set; }

        [ForeignKey("CounterOfferId")]
        public virtual Offer CounterOffer { get; set; }

        public virtual IList<OfferComment> OfferComments { get; set; }
    }
}
