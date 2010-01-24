using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using GMSBlog.Service;

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
    }
}
