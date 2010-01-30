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
    public class PostComment
    {
        public int PostId { get; set; }
        public Comment Comment { get; set; }

        public PostComment() { }
        public PostComment(int postId, Comment comment)
        {
            PostId = postId;
            Comment = comment;
        }
    }
}
