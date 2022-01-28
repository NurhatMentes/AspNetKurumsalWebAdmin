using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }
        public string title { get; set; }
        public string Context { get; set; }
        public string ImgUrl { get; set; }


        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}