﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Web;
using GMSBlog.Web.Controllers;
using StructureMap;
using GMSBlog.Web.Support;
using GMSBlog.Model.Entities;
using GMSBlog.Service;
using GMSBlog.Web.Tests.Helpers;
using GMSBlog.Model.Validation;
using GMSBlog.Web.Helpers;
using System.Web;
using Moq;
using System.Web.Routing;

namespace GMSBlog.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        #region Helpers
        [TestInitialize]
        public void InitializeStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.UseDefaultStructureMapConfigFile = false;
                x.AddRegistry(new GMSBlogRegistry());
            });
        }

        public Post DummyLivePost()
        {
            return new Post()
            {
                Content = "This is a dummy blog post. Blah blah blah blah.",
                IsPublished = true,
                Keywords = "",
                Summary = "A dummy post",
                Title = "Dummy Post"
            };
        }

        public Comment DummyLiveComment()
        {
            return new Comment()
            {
                Content = "Test",
                Name = "Mr Test"                
            };
        }

        #endregion

        #region Index

        [TestMethod]
        public void HomeController_Has_Index_Method_Which_Accepts_A_Page_Number()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Has_Index_Method_Which_Accepts_A_Page_Number_And_Returns_That_Page_Of_Posts()
        {
            // Arrange
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 0; i < 15; i++)
                {
                    repository.Save(DummyLivePost());
                }
            }
            var controller = new HomeController();

            // Act
            var result = controller.Index(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Post>));
            Assert.AreEqual(10, (result.ViewData.Model as IList<Post>).Count);
        }

        [TestMethod]
        public void HomeController_Has_Index_Method_Which_Accepts_A_Page_Number_And_Returns_That_Page_Of_Posts_And_Last_Page_Returns_Tail()
        {
            // Arrange
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 0; i < 15; i++)
                {
                    repository.Save(DummyLivePost());
                }
            }
            var controller = new HomeController();

            // Act
            var result = controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Post>));
            Assert.AreEqual(5, (result.ViewData.Model as IList<Post>).Count);
        }

#endregion

        #region Post

        [TestMethod]
        public void HomeController_Has_A_Post_Method_Which_Accepts_An_Id()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Post(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Has_A_Post_Method_Which_Accepts_An_Id_And_Returns_The_Post_With_That_Id()
        {
            // Arrange
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 0; i < 15; i++)
                {
                    repository.Save(DummyLivePost());
                }
            }
            var controller = new HomeController();

            // Act
            var result = controller.Post(8) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Post));
            Assert.AreEqual(8, (result.ViewData.Model as Post).Id);
        }

        [TestMethod]
        public void HomeController_Has_A_Post_Method_Which_Accepts_An_Id_And_Returns_The_Post_With_That_Id_And_Can_Get_Comments()
        {
            // Arrange
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var category = new Category() { Name = "Test Category" };

                for (int i = 0; i < 15; i++)
                {
                    var post = DummyLivePost();

                    post.Categories.Add(category);

                    for (int j = 0; j < 5; j++)
                    {
                        var comment = DummyLiveComment();

                        post.Comments.Add(comment);

                        repository.Save(comment);
                    }

                    repository.Save(post);
                }
                repository.Save(category);
            }
            var controller = new HomeController();

            // Act
            var result = controller.Post(8) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Post));
            var modelPost = (result.ViewData.Model as Post);

            Assert.AreEqual(8, modelPost.Id);
            Assert.AreEqual(5, modelPost.Comments.Count);
        }

        [TestMethod]
        public void HomeController_Has_A_Post_Method_Which_Accepts_The_Name_Year_Month_And_Day_Of_The_Post()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.PostByName("Test", 2010, 01, 31);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Has_A_Post_Method_Which_Accepts_The_Name_Year_Month_And_Day_Of_The_Post_And_Returns_The_Post_With_That_Id()
        {
            // Arrange
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 1; i <= 15; i++)
                {
                    var post = DummyLivePost();
                    post.Title = String.Format("Test{0}", i);
                    repository.Save(post);
                }
            }
            var controller = new HomeController();

            // Act
            var date = DateTime.Today;
            var result = controller.PostByName("Test8", date.Year, date.Month, date.Day) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Post));
            Assert.AreEqual(8, (result.ViewData.Model as Post).Id);
        }



        #endregion

        #region Category

        [TestMethod]
        public void Home_Controller_Has_An_Action_Named_Category_Which_Accepts_An_Id()
        {
            var controller = new HomeController();

            var result = controller.Category(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Home_Controller_Has_An_Action_Named_Category_Which_Accepts_An_Id_And_Returns_Posts_In_The_Category()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var category = new Category() { Name = "Test" };

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.Categories.Add(category);

                    repository.Save(post);
                }

                repository.Save(category);
            }

            var controller = new HomeController();

            var result = controller.Category(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Post>));
            Assert.AreEqual(5, (result.ViewData.Model as IList<Post>).Count);
        }

        #endregion

        #region About Page

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion

        #region Add Comment

        [TestMethod]
        public void HomeController_Has_An_Add_Comment_Method_Accepting_A_Comment_And_Post_Id()
        {
            var controller = new HomeController();

            var result = controller.AddComment(1, new Comment());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Has_An_Add_Comment_Method_Accepting_A_Comment_And_Post_Id_Which_Adds_A_Comment_To_A_Post()
        {
            var postId = 0;
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var post = DummyLivePost();
                repository.Save(post);

                postId = post.Id;

                Assert.AreEqual(0, repository.GetCommentsByPost(postId).Count);
            }

            var controller = new HomeController();

            controller.AddComment(postId, new Comment() { Name = "Test", Content = "Test" });

            DatabaseHelpers.Initialize(false);

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                Assert.AreEqual(1, repository.GetCommentsByPost(postId).Count);
            }
        }

        [TestMethod]
        public void HomeController_Has_An_Add_Comment_Method_Which_Redirects_To_The_Post_Page_On_Success()
        {
            var postId = 0;
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var post = DummyLivePost();
                repository.Save(post);

                postId = post.Id;
            }

            var controller = new HomeController();

            var result = controller.AddComment(postId, new Comment() { Name = "Test", Content = "Test" }) as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Post", result.RouteValues["action"]);
            Assert.AreEqual(postId, result.RouteValues["id"]);
        }

        [TestMethod]
        public void HomeController_Has_An_Add_Comment_Method_Which_Redirects_To_The_Post_Page_On_Failure_With_TempData_To_Reset_Fields()
        {
            var postId = 0;
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var post = DummyLivePost();
                repository.Save(post);

                postId = post.Id;
            }

            var controller = new HomeController();

            var result = controller.AddComment(postId, new Comment() { Name = "Test", Content="", Website="" });
            var redirect = result as RedirectToRouteResult;

            Assert.AreEqual("Home", redirect.RouteValues["controller"]);
            Assert.AreEqual("Post", redirect.RouteValues["action"]);
            Assert.AreEqual(postId, redirect.RouteValues["id"]);

            Assert.AreEqual("Test", controller.TempData["Name"]);
            Assert.AreEqual("", controller.TempData["Website"]);
            Assert.AreEqual("", controller.TempData["Content"]);
            Assert.AreEqual(1, (controller.TempData["CommentError"] as IEnumerable<RuleViolation>).Count());
        }

        

        #endregion

        #region RSS Feed

        [TestMethod]
        public void HomeController_Has_A_Feed_Method()
        {
            var controller = new HomeController();

            var result = controller.Feed();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Has_A_Feed_Method_Which_Returns_An_RssActionResult()
        {
            var controller = new HomeController();

            var result = controller.Feed();

            Assert.IsInstanceOfType(result, typeof(RssActionResult));
        }

        [TestMethod]
        public void HomeController_Has_A_Feed_Method_Which_Returns_An_RssActionResult_From_A_Feed_Of_Items()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 1; i <= 15; i++)
                {
                    var post = DummyLivePost();
                    post.Title = String.Format("Test{0}", i);
                    repository.Save(post);
                }
            }
                        
            //var controller = new HomeController();

            //controller.SetFakeControllerContext();

            //MvcMockHelpers.SetupRequestUrl(controller.Request, "~/Feed/");

            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Url).Returns(new Uri("http://www.gmsblog.com/Feed/"));

            

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()));


            var result = controller.Feed() as RssActionResult;

            Assert.AreEqual(15, result.Feed.Items.Count());
        }

        #endregion
    }
}
