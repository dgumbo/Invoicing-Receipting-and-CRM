using invoice_app.Basic.models;
using invoice_demo_app.Basic.models;
using invoice_demo_app.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoice_demo_app.Pdf.models
{ 
    public class Invoice : ItemWithList <InvoiceLine>
    {
        //@Column(name = "_date")
        [Column("invoice_date", TypeName = "DateTime")]
        [Required]
        public DateTime Date { get; set; }

        //@Column(name = "_number", nullable = false)
        [Column("invoice_number", TypeName = "nVarChar(25)")]
        [Required]
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        //@ManyToOne("ship_to_id", targetEntity = Address.class) 
        [ForeignKey("ship_to_id")]
        public Address ShipTo { get; set; }

        //@ManyToOne(targetEntity = Address.class) 
        [ForeignKey("bill_to_id")]
        public Address BillTo { get; set; }

        //@OneToMany(targetEntity=InvoiceLine.class, cascade=CascadeType.ALL )  
        public List<InvoiceLine> InvoiceLines { get; set; }

        [Column("end_notes", TypeName = "nVarChar(Max)")]
        public string EndNotes { get; set; }

        [Column("payment_details", TypeName = "nVarChar(Max)")]
        public string PaymentDetails { get; set; }

        public override List<InvoiceLine> GetListItems()
        {
            return InvoiceLines;
        } 
    }
}