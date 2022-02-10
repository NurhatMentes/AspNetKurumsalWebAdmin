using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [DisplayName("Slider Başlık"),StringLength(30,ErrorMessage = "En fazla 30 karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Slider Açıklama"), StringLength(150, ErrorMessage = "En fazla 150 karakter olmalıdır.")]
        public string Description { get; set; }

        [DisplayName("Slider Resim")]
        public string ImgUrl { get; set; }
    }
}