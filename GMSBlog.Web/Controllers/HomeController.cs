using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using GMSBlog.Service;
using GMSBlog.Model.Entities;

namespace GMSBlog.Web.Controllers
{
    [HandleError]
    public partial class HomeController : BaseBlogController
    {
        private const int _pageSize = 10;

        public virtual ActionResult Index(int? page)
        {
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var posts = repository.GetPublishedPostsPaged(_pageSize, page.GetValueOrDefault(1));

                return View(posts);
            }
        }

        public virtual ActionResult Post(int id)
        {
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var post = repository.GetPublishedPostById(id);

                if (post != null)
                {
                    return View(post);
                }
                else
                {
                    return RedirectToAction(Actions.Index());
                }
            }
        }

        public virtual ActionResult Category(int id)
        {
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var posts = repository.GetPublishedPostsByCategory(id);

                return View(posts);
            }
        }
        public virtual ActionResult About()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AddComment(int postId, Comment comment)
        {
            try
            {
                using (var repository = ObjectFactory.GetInstance<IBlogService>())
                {
                    var post = repository.GetPostById(postId);

                    repository.Save(comment);
                    post.Comments.Add(comment);
                    repository.Save(post);
                }


            }
            catch (InvalidOperationException ex)
            {
                TempData["Name"] = comment.Name;
                TempData["Content"] = comment.Content;
                TempData["Website"] = comment.Website;
                TempData["CommentError"] = comment.RuleViolations;
            }
            return RedirectToAction(Actions.Post(postId));
        }

        public string GetCommentDescriptionString(Comment comment)
        {
            if (string.IsNullOrEmpty(comment.Website) && string.IsNullOrEmpty(comment.Email))
            {
                return String.Format("Posted by {0} on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated);
            }
            else if (!string.IsNullOrEmpty(comment.Website) && string.IsNullOrEmpty(comment.Email))
            {
                return String.Format("Posted by <a href=\"{2}\">{0}</a> on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated, comment.Website);
               
            }
            else if (string.IsNullOrEmpty(comment.Website) && !string.IsNullOrEmpty(comment.Email))
            {
                return String.Format("Posted by <a href=\"mailto:{2}\">{0}</a> on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated, comment.Email);
            }
            else
            {
                return String.Format("Posted by <a href=\"mailto:{2}\">{0}</a> (<a href=\"{3}\">{3}</a>) on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated, comment.Email, comment.Website);
            }
        }
    }
}
