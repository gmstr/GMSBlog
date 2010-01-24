using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Model.Entities;

namespace GMSBlog.Model.Tests
{
    /// <summary>
    /// Summary description for CategoryTests
    /// </summary>
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void Category_Class_Exists()
        {
            var category = new Category();

            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void Category_Has_Name()
        {
            var category = new Category();

            category.Name = "Name";

            Assert.AreEqual("Name", category.Name);
        }

        [TestMethod]
        public void Category_Should_Have_List_Of_Posts()
        {
            var category = new Category();

            var posts = new List<Post>();

            category.Posts = posts;

            Assert.AreEqual(posts, category.Posts);
        }

        [TestMethod]
        public void Category_Should_Have_Initialised_List_Of_Posts()
        {
            var category = new Category();

            Assert.IsNotNull(category.Posts);
        }

        [TestMethod]
        public void Category_Can_Have_Posts_Assigned()
        {
            var category = new Category();

            category.Posts.Add(new Post());

            Assert.AreEqual(1, category.Posts.Count());
        }

        [TestMethod]
        public void Category_Post_Should_Have_Have_Category_Assigned()
        {
            var category = new Category();

            var post = new Post();

            category.AddPost(post);

            Assert.AreEqual(1, category.Posts.Count());

            Assert.AreEqual(1, post.Categories.Count());
        }

        [TestMethod]
        public void Category_Post_Can_Be_Removed()
        {
            var category = new Category();

            var post = new Post();

            category.AddPost(post);

            Assert.AreEqual(1, category.Posts.Count());

            Assert.AreEqual(1, post.Categories.Count());

            category.RemovePost(post);

            Assert.AreEqual(0, category.Posts.Count());

            Assert.AreEqual(0, post.Categories.Count());
        }

        [TestMethod]
        public void Category_Has_Id()
        {
            var category = new Category();

            category.Id = 1;

            Assert.AreEqual(1, category.Id);
        }
    }
}
