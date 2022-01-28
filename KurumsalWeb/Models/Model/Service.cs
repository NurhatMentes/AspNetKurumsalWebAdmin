using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [DisplayName("Hizmet Başlık")]
        [Required, StringLength(150, ErrorMessage = "En fazla 150 karekter olabilir.")]  
        public string Title { get; set; }

        [DisplayName("Hizmet Açıklama")]
        public string Description { get; set; }

        [DisplayName("Hizmet Resim")]
        public string ImgUrl { get; set; }
    }
}