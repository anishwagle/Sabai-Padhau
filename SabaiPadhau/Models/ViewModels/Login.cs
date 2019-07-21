using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SabaiPadhau.Models.ViewModels
{
    public class Login
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Confim Password")]
        public string ConfirmPassword { get; set; }
    }
}