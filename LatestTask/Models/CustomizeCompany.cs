using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LatestTask.Models
{
    [MetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {
        //add product
       // [Required]
       // [Compare("Email",ErrorMessage ="Email is not match")]
        [Display(Name ="Confirm Email")]
        public string ConfEmail { get; set; }

        // [Required]
      //  [Compare("Password", ErrorMessage ="Password is not match")]
        [Display(Name = "Confirm Password")]
        public string ConfPass { get; set; }
    }
    public class CompanyMetaData
    {
        //edit properties

        [Display(Name = "ID")]
        public int CID { get; set; }

       
        [Display(Name = "Company")]
        [Required (ErrorMessage ="Company Should not be null")]
        public string CName { get; set; }

        [Display(Name = "type")]
        public string CType { get; set; }

        [Required]
        //[DataType(DataType.EmailAddress)]
        [RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}", ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        
        public string CUrl { get; set; }

        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:D}",ApplyFormatInEditMode =false)]
        public Nullable<System.DateTime> Edate { get; set; }
        [Required]
        [StringLength(15,MinimumLength =5,ErrorMessage = "Should between 5-15 characters")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})", ErrorMessage = "Password is not strong")]
        
        public string Password { get; set; }

        [DisplayFormat (DataFormatString ="{0:N}", ApplyFormatInEditMode =false)]
        [Range(1000,100000,ErrorMessage ="Capital should btween 1000-100000")]
        public Nullable<int> Capital { get; set; }
        public string Logo { get; set; }

    }
}