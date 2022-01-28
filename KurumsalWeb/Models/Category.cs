using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Profile;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Models
{
    [Table("Category")] 
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, StringLength(50, ErrorMessage = "En fazla 50 karekter olabilir.")]
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}