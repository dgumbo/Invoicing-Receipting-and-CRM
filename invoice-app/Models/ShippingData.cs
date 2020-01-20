using invoice_demo_app.Basic.models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/**
*
* @author dgumbo
*/
namespace invoice_demo_app.Models
{
    public class ShippingData : BaseEntity
    {
        //private static   SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");

        [Column("ship_number", TypeName = "nVarChar(25)")]
        public string shipNumber { get; set; }

        [Column("sales_rep", TypeName = "nVarChar(50)")]
        public string salesRep { get; set; }

        //@Temporal(TemporalType.TIMESTAMP)
        //@DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
        [Column("ship_date", TypeName = "DateTime")]
        public DateTime shipDate { get; set; }

        [Column("ship_via", TypeName = "nVarChar(50)")]
        public string shipVia { get; set; }

        [Column("terms", TypeName = "nVarChar(100)")]
        public string terms { get; set; }

        [Column("due_date", TypeName = "DateTime")]
        public DateTime dueDate { get; set; }

        //public string getShipDatestring()
        //{
        //    return sdf.format(this.shipDate);
        //}

        //public string getDueDatestring()
        //{
        //    return sdf.format(this.dueDate);
        //}
    }
}
