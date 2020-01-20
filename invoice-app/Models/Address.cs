using invoice_demo_app.Basic.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
/**
*
* @author dgumbo
*/
namespace invoice_demo_app.Models
{
    public class Address : BaseEntity
    {
        [Column("title", TypeName = "nVarChar(25)")]
        public string Title { get; set; }

        [Column("firstname", TypeName = "nVarChar(50)")]
        public string Firstname { get; set; }

        [Column("lastname", TypeName = "nVarChar(50)")]
        public string Lastname { get; set; }

        [Column("Address1", TypeName = "nVarChar(50)")]
        public string Address1 { get; set; }

        [Column("Address2", TypeName = "nVarChar(50)")]
        public string Address2 { get; set; }

        [Column("Address3", TypeName = "nVarChar(50)")]
        public string Address3 { get; set; }

        [Column("city", TypeName = "nVarChar(50)")]
        public string City { get; set; }

        [Column("country", TypeName = "nVarChar(50)")]
        public string Country { get; set; }

        public string Fullname
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Title != null && Title.Trim().Length != 0)
                {
                    sb.Append(Title).Append(" ");
                }
                if (Firstname != null && Firstname.Trim().Length != 0)
                {
                    sb.Append(Firstname).Append(" ");
                }
                if (Lastname != null && Lastname.Trim().Length != 0)
                {
                    sb.Append(Lastname);
                }
                return sb.ToString();
            }
        }

        public bool hasAddress1()
        {
            return Address1 != null && Address1.Trim().Length != 0;
        }

        public bool hasAddress2()
        {
            return Address2 != null && Address2.Trim().Length != 0;
        }

        public bool hasAddress3()
        {
            return Address3 != null && Address3.Trim().Length != 0;
        }

        public bool hasCity()
        {
            return City != null && City.Trim().Length != 0;
        }

        public bool hasCountry()
        {
            return Country != null && Country.Trim().Length != 0;
        }

        public bool hasFullName()
        {
            return Fullname != null && Fullname.Trim().Length != 0;
        }
    }
}