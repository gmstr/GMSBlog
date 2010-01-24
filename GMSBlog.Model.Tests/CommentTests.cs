using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Model.Entities;
using GMSBlog.Model.Validation;

namespace GMSBlog.Model.Tests
{
    /// <summary>
    /// Summary description for CommentTests
    /// </summary>
    [TestClass]
    public class CommentTests
    {
        public Comment DummyLiveComment()
        {
            return new Comment()
            {
                Content = "Dummy comment.",
                Name = "Name",
                Website = "http://www.test.com"
            };
        }

        [TestMethod]
        public void Comment_Class_Exists()
        {
            var comment = new Comment();

            Assert.IsNotNull(comment);
        }

        [TestMethod]
        public void Comment_Has_Name()
        {
            var comment = new Comment();

            comment.Name = "Name";

            Assert.AreEqual("Name", comment.Name);
        }

        [TestMethod]
        public void Comment_Has_Website()
        {
            var comment = new Comment();

            comment.Website = "http://test.com";

            Assert.AreEqual("http://test.com", comment.Website);
        }

        [TestMethod]
        public void Comment_Has_Content()
        {
            var comment = new Comment();

            comment.Content = "Dummy Content";

            Assert.AreEqual("Dummy Content", comment.Content);
        }

        [TestMethod]
        public void Comment_Implements_IValidated()
        {
            var comment = new Comment();

            IValidated test = comment as IValidated;

            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void Comment_Is_Invalid_When_Name_Is_Empty()
        {
            var comment = DummyLiveComment();

            comment.Name = string.Empty;

            Assert.IsFalse(comment.IsValid);
        }

        [TestMethod]
        public void Comment_Is_Invalid_When_Content_Is_Empty()
        {
            var comment = DummyLiveComment();

            comment.Content = string.Empty;

            Assert.IsFalse(comment.IsValid);
        }

        [TestMethod]
        public void If_Website_Is_Not_Provided_The_Comment_Is_Still_Valid()
        {
            var comment = DummyLiveComment();

            comment.Website = string.Empty;

            Assert.IsTrue(comment.IsValid);
        }

        [TestMethod]
        public void If_Website_Is_Provided_But_Does_Not_Resolve_The_Comment_Is_Invalid()
        {
            var comment = DummyLiveComment();

            comment.Website = @"goolge";

            Assert.IsFalse(comment.IsValid);
        }

        [TestMethod]
        public void If_Website_Is_Provided_And_Does_Resolve_The_Comment_Is_Valid()
        {
            var comment = DummyLiveComment();

            comment.Website = @"http://www.google.com";

            Assert.IsTrue(comment.IsValid);
        }

        [TestMethod]
        public void If_http_Is_Not_Provided_The_Comment_Should_Be_Invalid()
        {
            var comment = DummyLiveComment();

            comment.Website = @"www.google.com";

            Assert.IsFalse(comment.IsValid);
        }

        [TestMethod]
        public void Comment_Has_Post_Associated_With_It()
        {
            var comment = new Comment();

            var post = new Post();

            post.AddComment(comment);

            Assert.AreEqual(post, comment.Post);
        }

        [TestMethod]
        public void Can_Add_Comments_To_Post()
        {
            var post = new Post();

            var comment = new Comment();

            post.AddComment(comment);

            Assert.AreEqual(comment.Post, post);

            Assert.AreEqual(post.Comments.Count, 1);
        }

        [TestMethod]
        public void Comment_Has_Id()
        {
            var comment = new Comment();

            comment.Id = 1;

            Assert.AreEqual(1, comment.Id);
        }

        [TestMethod]
        public void Comment_Has_DateCreated()
        {
            var commentType = typeof(Comment);

            var properties = commentType.GetProperties();

            Assert.IsTrue(properties.Any(x => x.Name == "DateCreated"));

            Assert.IsTrue(properties.Any(x => x.PropertyType == typeof(DateTime)));
        }

        [TestMethod]
        public void DateCreated_Should_Be_Set_On_Construction()
        {
            var comment = new Comment();

            Assert.IsTrue(comment.DateCreated > DateTime.Now.AddSeconds(-2), String.Format("DateCreated: {0}, Actual (less 2 seconds): {1}", comment.DateCreated, DateTime.Now.AddSeconds(-2)));
        }
    }
}
