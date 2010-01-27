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
        public ActionResult AddComment(int postId, Comment comment)
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
    }
}
