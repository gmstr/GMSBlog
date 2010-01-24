using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using GMSBlog.Web.Models;
using StructureMap;
using GMSBlog.Service;

namespace GMSBlog.Web.Controllers
{
    public abstract partial class BaseBlogController : Controller
    {
        public BaseBlogController()
        {
            var repository = ObjectFactory.GetInstance<IBlogService>();
            var summaries = new List<CategorySummary>();

            repository.GetCategories().OrderByDescending(x => x.PublishedPosts.Count()).ToList().ForEach(x => summaries.Add(new CategorySummary(x)));

            ViewData["Categories"] = summaries;

        }
    }
}
