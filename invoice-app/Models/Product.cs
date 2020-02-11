using invoice_demo_app.Basic.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/**
*
* @author dgumbo
*/
namespace invoice_demo_app.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [Column("_number", TypeName = "nVarChar(25)")]
        public string Number { get; set; }

        [Required]
        [Column(TypeName = "nVarChar(1000)")]
        public string Name { get; set; }

        [Column(TypeName = "nVarChar(Max)")]
        public string Description { get; set; }
    }
}
