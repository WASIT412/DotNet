using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Corporate
{
    [MetadataType(typeof(PurchaserMetaData))]
    public partial class Purchaser
    {
    }
    public class PurchaserMetaData
    {
       
        [Display(Name = "Firm Name")]
        public string PurchaserNo { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
         [Required]
        public string LastName { get; set; }
         [Required]
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
         [Required]
        public string ContactNo { get; set; }
         [Required]
        public string EmailID { get; set; }
        public string Gender { get; set; }
    }
}