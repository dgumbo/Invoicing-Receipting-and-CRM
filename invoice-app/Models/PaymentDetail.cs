using invoice_demo_app.Basic.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_demo_app.Models
{
    public class PaymentDetail : BaseEntity
    { 
        [Required]
        [Column(TypeName = "nVarChar(100)")]
        public string CardOwnerName { get; set; }

        [Required]
        [Column(TypeName = "VarChar(16)")]
        public string CardNumber { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "VarChar(3)")]
        public string CVV { get; set; }
    }
}
