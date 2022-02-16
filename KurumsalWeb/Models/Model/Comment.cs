using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Comment")]
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        [DisplayName("Ad - Soyad")]
        public string FirstLastName { get; set; }

        [Required]
        [DisplayName("E Posta")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Yorumunuz")]
        public string CommentContent { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}