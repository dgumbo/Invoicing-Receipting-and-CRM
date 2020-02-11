using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/**
*
* @author denzil
*/
namespace invoice_demo_app.Basic.models
{
    [Serializable]
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("created_by_user", TypeName = "nVarChar(100)")]
        [Editable(false, AllowInitialValue = true)]
        public string CreatedByUser { get; set; }

        [Required]
        [Column("creation_time", TypeName = "DateTime")]
        [Editable(false, AllowInitialValue = true)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }

        [Required]
        [Column("modified_by_user", TypeName = "nVarChar(100)")]
        public string ModifiedByUser { get; set; }

        [Required]
        [Column("modification_time", TypeName = "DateTime")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ModificationTime { get; set; }

        [Required]
        [Column("active_status", TypeName = "bit")]
        public bool ActiveStatus { get; set; }

        public virtual void ValidateBaseEntityProperties()
        {
            DateTime currentTime = DateTime.Now;
            if (CreatedByUser == null) { CreatedByUser = "dgumbo"; ActiveStatus = true; }
            if (CreationTime != null && CreationTime.Year < 1900) { CreationTime = currentTime; }

            ModificationTime = currentTime;
            ModifiedByUser = "dgumbo";
        }
    }
}
