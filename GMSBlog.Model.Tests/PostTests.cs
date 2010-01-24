using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Model.Entities;
using GMSBlog.Model.Validation;
using System.Threading;

namespace GMSBlog.Model.Tests
{    
    /// <summary>
    /// Summary description for PostTests
    /// </summary>
    [TestClass]
    public class PostTests
    {
        public Post DummyLivePost()
        {
            return new Post()
            {
                Content = "This is a dummy blog post. Blah blah blah blah.",
                IsPublished = true,
                Summary = "A dummy post",
                Title = "Dummy Post"
            };
        }

        [TestMethod]
        public void Post_Class_Exists()
        {
            var post = new Post();

            Assert.IsNotNull(post);
        }

        [TestMethod]
        public void Post_Contains_Title()
        {
            var post = new Post();

            post.Title = "Test";

            Assert.AreEqual("Test", post.Title);
        }

        [TestMethod]
        public void Post_Contains_Summary()
        {
            var post = new Post();

            post.Summary = "Summary";

            Assert.AreEqual("Summary", post.Summary);
        }

        [TestMethod]
        public void Post_Contains_Content()
        {
            var post = new Post();

            post.Content = "New Post Detail";

            Assert.AreEqual("New Post Detail", post.Content);
        }

        [TestMethod]
        public void Post_Contains_DateCreated()
        {
            var post = new Post();

            Assert.IsNotNull(post.DateCreated);
        }

        [TestMethod]
        public void Post_Contains_DateUpdated()
        {
            var post = new Post();

            Assert.IsNotNull(post.DateUpdated);
        }

        [TestMethod]
        public void Post_Contains_IsPublished()
        {
            var post = DummyLivePost();

            post.IsPublished = true;

            Assert.AreEqual(true, post.IsPublished);
        }

        [TestMethod]
        public void Post_Implements_IValidated()
        {
            var post = new Post();

            IValidated test = post as IValidated;

            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void Post_Is_Invalid_When_Title_Is_Empty()
        {
            var post = DummyLivePost();

            post.Title = string.Empty;

            Assert.IsFalse(post.IsValid);
        }

        [TestMethod]
        public void Post_Is_Invalid_When_Summary_Is_Empty()
        {
            var post = DummyLivePost();

            post.Summary = string.Empty;

            Assert.IsFalse(post.IsValid);
        }

        [TestMethod]
        public void Post_Is_Invalid_When_Detail_Is_Empty()
        {
            var post = DummyLivePost();

            post.Content = string.Empty;

            Assert.IsFalse(post.IsValid);
        }

        [TestMethod]
        public void Post_Cannot_Be_Published_If_Invalid()
        {
            var post = DummyLivePost();

            Assert.IsTrue(post.IsValid,"Post is not initially valid");
            Assert.IsTrue(post.IsPublished, "Post is not initially published");

            post.Content = string.Empty;

            Assert.IsFalse(post.IsValid, "Post has not been invalidated");
            Assert.IsFalse(post.IsPublished, "Post is still considered published");
        }

        [TestMethod]
        public void DateCreated_Should_Be_Set_On_Construction()
        {
            var post = new Post();

            Assert.IsTrue(post.DateCreated > DateTime.Now.AddSeconds(-2), String.Format("DateCreated: {0}, Actual (less 2 seconds): {1}", post.DateCreated, DateTime.Now.AddSeconds(-2)));
        }

        [TestMethod]
        public void DateUpdated_Should_Be_Set_On_Construction()
        {
            var post = new Post();

            Assert.IsTrue(post.DateUpdated > DateTime.Now.AddSeconds(-2), String.Format("DateCreated: {0}, Actual (less 2 seconds): {1}", post.DateUpdated, DateTime.Now.AddSeconds(-2)));
        }

        [TestMethod]
        public void DateUpdated_Should_Be_Equal_To_Date_Created_For_A_New_Post()
        {
            var post = new Post();

            Thread.Sleep(1);

            Assert.AreEqual(post.DateCreated, post.DateUpdated);
        }

        [TestMethod]
        public void DateUpdated_Should_Change_If_Title_Is_Modified_Later()
        {
            var post = new Post();

            Thread.Sleep(1);

            post.Title = "New Title";

            Assert.AreNotEqual(post.DateCreated, post.DateUpdated);
        }

        [TestMethod]
        public void DateUpdated_Should_Change_If_Summary_Is_Modified_Later()
        {
            var post = new Post();

            Thread.Sleep(1);

            post.Summary = "New Summary";

            Assert.AreNotEqual(post.DateCreated, post.DateUpdated);
        }

        [TestMethod]
        public void DateUpdated_Should_Change_If_Content_Is_Modified_Later()
        {
            var post = new Post();

            Thread.Sleep(1);

            post.Content = "New Content";

            Assert.AreNotEqual(post.DateCreated, post.DateUpdated);
        }

        [TestMethod]
        public void DateUpdated_Should_Not_Change_If_Post_Is_Published()
        {
            var post = new Post();

            Thread.Sleep(1);
            
            post.IsPublished = true;

            Assert.AreEqual(post.DateCreated, post.DateUpdated);
        }

        [TestMethod]
        public void Post_Should_Have_List_Of_Categories()
        {
            var post = new Post();

            var categories = new List<Category>();

            post.Categories = categories;

            Assert.AreEqual(categories, post.Categories);
        }

        [TestMethod]
        public void Post_Should_Have_Initialised_List_Of_Categories()
        {
            var post = new Post();

            Assert.IsNotNull(post.Categories);
        }

        [TestMethod]
        public void Post_Can_Have_Categories_Assigned()
        {
            var post = new Post();

            post.Categories.Add(new Category());

            Assert.AreEqual(1, post.Categories.Count());
        }

        [TestMethod]
        public void Post_Category_Should_Have_Have_Post_Assigned()
        {
            var post = new Post();

            var category = new Category();

            post.AddCategory(category);

            Assert.AreEqual(1, post.Categories.Count());

            Assert.AreEqual(1, category.Posts.Count());
        }

        [TestMethod]
        public void Post_Category_Can_Be_Removed()
        {
            var post = new Post();

            var category = new Category();

            post.AddCategory(category);

            Assert.AreEqual(1, post.Categories.Count());

            Assert.AreEqual(1, category.Posts.Count());

            post.RemoveCategory(category);

            Assert.AreEqual(0, post.Categories.Count());

            Assert.AreEqual(0, category.Posts.Count());
        }

        [TestMethod]
        public void Post_Should_Have_List_Of_Comments()
        {
            var post = new Post();

            var comments = new List<Comment>();

            post.Comments = comments;

            Assert.AreEqual(comments, post.Comments);
        }

        [TestMethod]
        public void Post_Should_Have_Initialised_List_Of_Comments()
        {
            var post = new Post();

            Assert.IsNotNull(post.Comments);
        }

        [TestMethod]
        public void Post_Comments_Can_Be_Assigned()
        {
            var post = new Post();

            var comment = new Comment() { Name = "test", Content = "test" };

            post.AddComment(comment);

            Assert.AreEqual(1, post.Comments.Count);
            Assert.AreEqual(post, comment.Post);
        }

        [TestMethod]
        public void Post_Comments_Can_Be_Removed()
        {
            var post = new Post();

            var comment = new Comment() { Name = "test", Content = "test" };

            post.AddComment(comment);

            Assert.AreEqual(1, post.Comments.Count);
            Assert.AreEqual(post, comment.Post);

            post.RemoveComment(comment);

            Assert.AreEqual(0, post.Comments.Count);
            Assert.AreEqual(null, comment.Post);
        }

        [TestMethod]
        public void Post_Has_Id()
        {
            var post = new Post();

            post.Id = 1;

            Assert.AreEqual(1, post.Id);
        }
    }
}
