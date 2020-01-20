
using invoice_demo_app.Basic.models;
using invoice_demo_app.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace invoice_demo_app.Pdf.models
{ 
    public class InvoiceLine : BaseEntity
    { 
        [ForeignKey("invoice_id")]
       // [JsonIgnore] 
        public virtual Invoice Invoice { get; set; } 

        [Required]
        [ForeignKey("product_id")]
        public Product Product { get; set; }

        [Required]
        [Column("quantity", TypeName = "int")]
        public long Quantity { get; set; }

        [Column("price", TypeName = "numeric(16, 6)")]
        public double Price { get; set; }

        public double addTotal(double totalCost)
        {
            return totalCost + this.getTotal();
        }

        public string getTotalstring()
        { 
            return getTotal().ToString("###,###.00");
        }

        public string getPricestring()
        { 
            return Price.ToString("###,###.00");
        }

        public string getQuantitystring()
        { 
            return Quantity.ToString("###,###.00");
        }

        public double getTotal()
        {
            return Price * Quantity;
        }
    }
}
