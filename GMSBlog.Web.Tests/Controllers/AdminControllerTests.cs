using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Web.Controllers;
using Moq;
using System.Web.Mvc;
using GMSBlog.Web.Tests.Helpers;
using StructureMap;
using GMSBlog.Web.Support;
using GMSBlog.Model.Entities;
using GMSBlog.Service;

namespace GMSBlog.Web.Tests.Controllers
{
    /// <summary>
    /// Summary description for AdminControllerTests
    /// </summary>
    [TestClass]
    public class AdminControllerTests
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

        #endregion
        
        #region Index

        [TestMethod]
        public void AdminController_Contains_Index_Method()
        {
            var controller = new AdminController();

            ActionResult result = controller.Index();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Categories
        [TestMethod]
        public void AdminController_Contains_Categories_Method()
        {
            var controller = new AdminController();

            ActionResult result = controller.Categories();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void AdminController_Categories_Method_Accepts_List_Categories()
        {
            var controller = new AdminController();

            var result = controller.Categories() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Category>));
        }

        #endregion

        #region Add Category

        [TestMethod]
        public void AdminController_Has_An_Add_Category_Get_Method()
        {
            var controller = new AdminController();

            var result = controller.AddCategory();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Category_Get_Method_Has_Model_Of_Category()
        {
            var controller = new AdminController();

            var result = controller.AddCategory() as ViewResult;

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Category));
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Category_Post_Method()
        {
            var controller = new AdminController();

            var result = controller.AddCategory(new Category() { Name = "Test" });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Category_Post_Method_Which_Saves_A_Category()
        {
            DatabaseHelpers.Initialize(true);
            InitializeStructureMap();
            var repository = ObjectFactory.GetInstance<IBlogService>();
            Assert.AreEqual(0, repository.GetCategories().Count);
            var controller = new AdminController();

            controller.AddCategory(new Category() { Name = "Test" });

            Assert.AreEqual(1, repository.GetCategories().Count);
            Assert.AreEqual("Test", repository.GetCategories().First().Name);
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Category_Post_Method_Which_On_Success_Redirects_To_Categories()
        {
            var controller = new AdminController();

            var result = controller.AddCategory(new Category() { Name = "Test" }) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Categories", result.RouteValues["action"]);
        }

        #endregion

        #region Delete Category

        [TestMethod]
        public void AdminController_Has_A_Delete_Category_Post_Method_Which_On_Success_Redirects_To_Categories()
        {
            DatabaseHelpers.Initialize(true);
            InitializeStructureMap();
            var repository = ObjectFactory.GetInstance<IBlogService>();
            var controller = new AdminController();
            controller.AddCategory(new Category() { Name = "Test" });
            var category = repository.GetCategories().First();

            var result = controller.DeleteCategory(category.Id) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Categories", result.RouteValues["action"]);
        }

        [TestMethod]
        public void AdminController_Has_A_Delete_Category_Post_Method_Which_Deletes_A_Category_With_Id()
        {
            DatabaseHelpers.Initialize(true);
            InitializeStructureMap();
            var repository = ObjectFactory.GetInstance<IBlogService>();
            var controller = new AdminController();
            Assert.AreEqual(0, repository.GetCategories().Count);
            controller.AddCategory(new Category() { Name = "Test" });
            Assert.AreEqual(1, repository.GetCategories().Count);
            var category = repository.GetCategories().First();

            controller.DeleteCategory(category.Id);

            Assert.AreEqual(0, repository.GetCategories().Count);
        }

        #endregion

        #region Posts

        [TestMethod]
        public void AdminController_Has_A_List_Post_Get_Method()
        {
            var controller = new AdminController();

            var result = controller.Posts();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_A_List_Post_Get_Method_That_Returns_Posts()
        {
            var controller = new AdminController();

            var result = controller.Posts(null) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Post>));
        }
               
        [TestMethod]
        public void AdminController_List_Post_Method_Accepts_A_Page_Number()
        {
            var controller = new AdminController();

            var result = controller.Posts(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IList<Post>));
        }

        [TestMethod]
        public void AdminController_List_Post_Method_Accepts_A_Page_Number_And_Gets_Results_Paged()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 0; i < 15; i++)
                {
                    repository.Save(DummyLivePost());
                }

                repository.CommitChanges();
            }
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var controller = new AdminController();

                var result = controller.Posts(1) as ViewResult;

                Assert.AreEqual(15, repository.GetPosts().Count);

                Assert.AreEqual(10, (result.ViewData.Model as IList<Post>).Count);
            }
        }
        [TestMethod]
        public void AdminController_List_Post_Method_Without_A_Page_Number_Gets_Page_1()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                for (int i = 0; i < 15; i++)
                {
                    repository.Save(DummyLivePost());
                }

                repository.CommitChanges();
            }
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var controller = new AdminController();

                var result = controller.Posts(null) as ViewResult;

                Assert.AreEqual(15, repository.GetPosts().Count);

                Assert.AreEqual(10, (result.ViewData.Model as IList<Post>).Count);
            }
        }

        #endregion

        #region Add Post

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Get_Method()
        {
            var controller = new AdminController();

            var result = controller.AddPost();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Get_Method_Which_Has_A_Post_Model()
        {
            var controller = new AdminController();

            var result = controller.AddPost() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Post));
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Post_Method_Which_Accepts_A_Post()
        {
            var controller = new AdminController();

            var result = controller.AddPost(DummyLivePost(), null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Post_Method_Which_Writes_A_Post_To_Db()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var controller = new AdminController();

                Assert.AreEqual(0, repository.GetPosts().Count);

                var post = DummyLivePost();

                controller.AddPost(post, null);

                Assert.AreEqual(1, repository.GetPosts().Count);
            }

        }

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Post_Method_Which_Redirects_To_Posts_On_Success()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var controller = new AdminController();

                Assert.AreEqual(0, repository.GetPosts().Count);

                var result = controller.AddPost(DummyLivePost(), null) as RedirectToRouteResult;

                Assert.AreEqual("Posts", result.RouteValues["action"]);
            }
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Post_Method_Which_Returns_To_Allow_Updates_On_Invalid_Input()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var controller = new AdminController();

                Assert.AreEqual(0, repository.GetPosts().Count);

                var post = DummyLivePost();

                post.Content = string.Empty;

                var result = controller.AddPost(post, null) as ViewResult;

                Assert.AreEqual(post, result.ViewData.Model as Post);
                Assert.AreEqual("AddPost", result.ViewName);
            }
        }

        [TestMethod]
        public void AdminController_Has_An_Add_Post_Post_Method_Accepting_A_Post_Object_And_Can_Assign_Categories()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {

                post = DummyLivePost();

                repository.Save(post);

                var category1 = new Category() { Name = "Cat1" };
                repository.Save(category1);
                var category2 = new Category() { Name = "Cat2" };
                repository.Save(category2);
                var category3 = new Category() { Name = "Cat3" };
                repository.Save(category3);

                repository.CommitChanges();

                Assert.AreEqual(0, post.Categories.Count);
            }

            var controller = new AdminController();

            var collection = new FormCollection();

            collection.Add("Category-1", "false");
            collection.Add("Category-2", "true false");
            collection.Add("Category-3", "true false");

            var result = controller.AddPost(post, collection) as ViewResult;

            Assert.AreEqual(2, post.Categories.Count);
        }

        #endregion

        #region Delete Post

        [TestMethod]
        public void AdminController_Has_A_Delete_Post_Post_Method_Which_Accepts_An_Id()
        {
            DatabaseHelpers.Initialize(true);
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }
            var controller = new AdminController();

            var result = controller.DeletePost(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_A_Delete_Post_Post_Method_Which_Accepts_An_Id_And_Deletes_That_Post()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                Assert.AreEqual(1, repository.GetPosts().Count);

                var controller = new AdminController();

                var result = controller.DeletePost(post.Id);

                Assert.AreEqual(0, repository.GetPosts().Count);

            }
        }

        [TestMethod]
        public void AdminController_Has_A_Delete_Post_Post_Method_Which_Redirects_To_Posts()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }
            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                Assert.AreEqual(1, repository.GetPosts().Count);

                var controller = new AdminController();

                var result = controller.DeletePost(post.Id) as RedirectToRouteResult;

                Assert.AreEqual("Posts",result.RouteValues["action"]);

            }
        }

        #endregion

        #region Edit Post

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Get_Method_Which_Accepts_An_Id()
        {
            var controller = new AdminController();

            var result = controller.EditPost(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Get_Method_Which_Accepts_An_Id_And_Returns_The_Post_With_That_Id()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }

            var controller = new AdminController();

            var result = controller.EditPost(post.Id) as ViewResult;

            Assert.AreEqual(post.Id, (result.ViewData.Model as Post).Id);
        }

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Post_Method_Accepting_A_Post_Object()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }

            var controller = new AdminController();

            var result = controller.EditPost(post, null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Post_Method_Accepting_A_Post_Object_Which_Saves_Modifications()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }

            var controller = new AdminController();

            post.Content = "Blarg";

            controller.EditPost(post, null);

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                var loadedPost = repository.GetPostById(post.Id);

                Assert.AreEqual("Blarg", loadedPost.Content);
            }
        }

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Post_Method_Accepting_A_Post_Object_Which_Then_Redirects_To_Post_List()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }

            var controller = new AdminController();

            var result = controller.EditPost(post, null) as RedirectToRouteResult;

            Assert.AreEqual("Posts", result.RouteValues["action"]);
        }

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Post_Method_Accepting_A_Post_Object_Which_Then_Redirects_To_Edit_If_Fails_To_Save()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {
                post = DummyLivePost();

                repository.Save(post);

                repository.CommitChanges();
            }

            var controller = new AdminController();

            post.Content = string.Empty;

            var result = controller.EditPost(post, null) as ViewResult;

            Assert.AreEqual(post, result.ViewData.Model as Post);
            Assert.AreEqual("AddPost", result.ViewName);
        }

        [TestMethod]
        public void AdminController_Has_An_Edit_Post_Post_Method_Accepting_A_Post_Object_And_Can_Assign_Categories()
        {
            DatabaseHelpers.Initialize(true);

            var post = new Post();

            using (var repository = ObjectFactory.GetInstance<IBlogService>())
            {

                post = DummyLivePost();

                repository.Save(post);

                var category1 = new Category() { Name = "Cat1" };
                repository.Save(category1);
                var category2 = new Category() { Name = "Cat2" };
                repository.Save(category2);
                var category3 = new Category() { Name = "Cat3" };
                repository.Save(category3);

                repository.CommitChanges();

                Assert.AreEqual(0, post.Categories.Count);
            }

            var controller = new AdminController();

            var collection = new FormCollection();

            collection.Add("Category-1", "false");
            collection.Add("Category-2", "true false");
            collection.Add("Category-3", "true false");

            var result = controller.EditPost(post, collection) as ViewResult;

            Assert.AreEqual(2, post.Categories.Count);
        }

        #endregion        

        #region Post Preview

        [TestMethod]
        public void AdminController_Has_A_Preview_Method_Which_Returns_Preview_Text()
        {
            var controller = new AdminController();

            var result = controller.GetPostPreview("Test preview");

            Assert.IsNotNull(result);

            Assert.AreEqual("<p>Test preview</p>".Trim(), (result as PartialViewResult).ViewData.Model.ToString().Trim());
        }

        #endregion
    }    
}
