using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Models
{
    public class OfferComment
    {
        public int OfferCommentId { get; set; }

        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }

        public DateTime Created { get; set; }

        public int? CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Person CreatedBy { get; set; }

        public string Details { get; set; }
    }
}
