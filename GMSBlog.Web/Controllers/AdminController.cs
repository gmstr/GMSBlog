using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using StructureMap;
using GMSBlog.Service;
using GMSBlog.Model.Entities;
using GMSBlog.Web.Helpers;

namespace GMSBlog.Web.Controllers
{
    [Authorize]
    public partial class AdminController : Controller
    {
        private int _pageSize = 10;

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Categories()
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            return View(repository.GetCategories());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AddCategoryLink()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AddCategory()
        {
            return View(new Category());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AddCategory(Category category)
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            repository.Save(category);

            repository.CommitChanges();

            return RedirectToAction("Categories");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult DeleteCategory(int id)
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            var category = repository.GetCategoryById(id);

            category.Posts.Clear();

            repository.Delete(category);

            repository.CommitChanges();

            return RedirectToAction(Actions.Categories());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Posts(int? page)
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            var posts = repository.GetPostsPaged(_pageSize, page.GetValueOrDefault(1));

            return View(posts);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AddPost()
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            ViewData["Categories"] = repository.GetCategories();

            return View(new Post());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AddPost(Post post, FormCollection collection)
        {
            return processPost(post, collection);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult DeletePost(int id)
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            repository.Delete(repository.GetPostById(id));

            repository.CommitChanges();

            return RedirectToAction(Actions.Posts(1));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult EditPost(int id)
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();

            ViewData["Categories"] = repository.GetCategories();

            return View("AddPost", repository.GetPostById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditPost(Post post, FormCollection collection)
        {
            return processPost(post, collection);
        }
        private ActionResult processPost(Post post, FormCollection collection)
        {
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                try
                {
                    var categories = repository.GetCategories();

                    foreach (var category in categories)
                    {
                        if (collection[String.Format("Category-{0}", category.Id)].Contains("true"))
                        {
                            if (!post.Categories.Select(x => x.Id).Contains(category.Id))
                            {
                                post.Categories.Add(category);
                            }
                        }
                        else
                        {
                            if (post.Categories.Select(x => x.Id).Contains(category.Id))
                            {
                                post.Categories.Remove(category);
                            }
                        }
                    }

                    repository.Save(post);

                    repository.CommitChanges();

                    return RedirectToAction(Actions.Posts());
                }
                catch (InvalidOperationException exception)
                {
                    ModelState.AddRuleViolations(post.RuleViolations);

                    return View("AddPost", post);
                }
            }
        }
        public virtual PartialViewResult GetPostPreview(string previewText)
        {
            return PartialView("GetPostPreview", MarkdownHelper.RenderMarkdown(previewText));
        }
    }
}
