using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [DisplayName("İsim")]
        public string FullName { get; set; }

        [DisplayName("İş")]
        public string Job { get; set; }

        [Required, StringLength(50, ErrorMessage = "En fazla 10 karekter olabilir.")]
        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [Required,StringLength(50,ErrorMessage = "En fazla 50 karekter olabilir.")]
        [DisplayName("E Posta")]
        public string Email { get; set; }

        [Required, StringLength(50, ErrorMessage = "En fazla 50 karekter olabilir.")]
        [DisplayName("Parola")]
        public string Password { get; set; }

        [DisplayName("Yetki")]
        public string Auth { get; set; }
    }
}