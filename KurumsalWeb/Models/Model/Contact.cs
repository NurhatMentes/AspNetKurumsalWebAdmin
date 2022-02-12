using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required, StringLength(300, ErrorMessage = "En fazla 300 karekter olabilir.")]
        [DisplayName("Adres")]
        public string Adress { get; set; }

        [Required, StringLength(15, ErrorMessage = "En fazla 15 karekter olabilir.")]
        [DisplayName("Telefon")]
        public string Tel { get; set; }

        [DisplayName("E-Posta")]
        public string ContactEmail { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
    }
}