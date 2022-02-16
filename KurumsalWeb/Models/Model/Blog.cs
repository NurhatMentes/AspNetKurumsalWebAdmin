using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }

        [DisplayName("Blog Başlık")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        public string Content { get; set; }

        [DisplayName("Ana Resim")]
        public string ImgUrl { get; set; }


        public int? CategoryId { get; set; }
        public Category Category { get; set; }


        private ICollection<Comment> Comments { get; set; }
    }
}