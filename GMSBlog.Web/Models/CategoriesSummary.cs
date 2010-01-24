using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using GMSBlog.Model.Entities;

namespace GMSBlog.Web.Models
{
    public class CategorySummary
    {
        public CategorySummary() { }

        public CategorySummary(Category category)
        {
            Name = category.Name;
            Id = category.Id;
            NoOfPosts = category.PublishedPosts.Count();
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public int NoOfPosts { get; set; }
    }
}
