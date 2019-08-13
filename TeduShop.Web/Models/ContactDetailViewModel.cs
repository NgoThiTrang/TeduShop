using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ContactDetailViewModel
    {
     
        public int ID { get; set; }
       [Required(ErrorMessage="Tên không được trống")]
        public string Name { get; set; }
        [MaxLength(50, ErrorMessage ="Không được quá 50 kí tự")]
        public string Phone { get; set; }
        [MaxLength(250, ErrorMessage = "Không được quá 250 kí tự")]
        public string Email { get; set; }


        [MaxLength(250, ErrorMessage = "Không được quá 250 kí tự")]
        public string Website { get; set; }
        [MaxLength(250, ErrorMessage = "Không được quá 250 kí tự")]
        public string Address { get; set; }
        public bool Status { get; set; }
       
        
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string Other { get; set; }


    }
}