using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("SiteIdentity")] 
    public class SiteIdentity
    {
        [Key]
        public int IdentityId { get; set; }

        [DisplayName("Site Başlık")]
        [Required,StringLength(100,ErrorMessage = "En fazla 100 karekter olabilir.")]
        public string Title { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(200,ErrorMessage = "En fazla 200 karekter olabilir.")]
        public string Keywords { get; set; }

        [DisplayName("Site Açıklama")]
        [Required, StringLength(400, ErrorMessage = "En fazla 400 karekter olabilir.")]
        public string Description { get; set; }

        [DisplayName("Site Logo")]
        public string LogoUrl { get; set; }

        [DisplayName("Site Ünvan")]
        public string Rank { get; set; }
    }
}