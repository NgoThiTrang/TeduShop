using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Bạn cần phải nhập đây đủ tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Bạn cần phải nhập đây đủ Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn cần phải nhập đây đủ Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn cần phải nhập đây đủ tên")]
        [MinLength(6, ErrorMessage ="password bắt buộc là 6 kí tự")]
        public string Password { get; set; }
        public  string Address { get; set; }
        [Required(ErrorMessage = "Bạn cần phải nhập đây đủ số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}