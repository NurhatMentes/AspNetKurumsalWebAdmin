using System;
using System.Collections.Generic;
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

        [Required,StringLength(50,ErrorMessage = "En fazla 50 karekter olabilir.")]
        public string Email { get; set; }

        [Required, StringLength(50, ErrorMessage = "En fazla 50 karekter olabilir.")]
        public string Password { get; set; }

        public string Auth { get; set; }
    }
}