using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using GMSBlog.Model.Entities;
using NHibernate.Tool.hbm2ddl;
using GMSBlog.Service.NHibernate.Mappings;
using GMSBlog.Service.NHibernate;
using System.Threading;

namespace GMSBlog.Service.Tests
{
    /// <summary>
    /// Summary description for NHibernateServiceTest
    /// </summary>
    [TestClass]
    public class NHibernateServiceTests
    {
        private const string connectionString = "NHibernateBlogService.Test";

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

        void Initialize()
        {
            Initialize(false);
        }
        void Initialize(bool empty)
        {
            SessionFactory = CreateSessionFactory(empty);
        }

        ISessionFactory SessionFactory;

        private ISessionFactory CreateSessionFactory(bool empty)
        {            
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey(connectionString)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMappings>())
                .ExposeConfiguration(c =>
                {                    
                    if (empty)
                    {
                        new SchemaExport(c)
                          .Create(false, true);
                    }
                });


            return cfg.BuildSessionFactory();
        }

        [TestMethod]
        public void Can_Initialise_The_Session_Factory()
        {
            Initialize(true);
        }

        [TestMethod]
        public void Can_Create_Repository()
        {
            var repository = new NHibernateBlogService();

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Can_Place_Repository_In_Using_Block()
        {
            using (var repository = new NHibernateBlogService())
            {

            }
        }

        [TestMethod]
        public void Can_Save_Post()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                repository.Save(DummyLivePost());
            }
        }

        [TestMethod]
        public void Can_Retrieve_Saved_Post()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                repository.Save(DummyLivePost());

                Assert.AreEqual(1, repository.GetPosts().Count);
            }
        }

        [TestMethod]
        public void Can_Save_Many_Posts()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    repository.Save(DummyLivePost());    
                }                
            }
        }

        [TestMethod]
        public void Can_Retrieve_Many_Saved_Posts()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    repository.Save(DummyLivePost());
                }

                Assert.AreEqual(10, repository.GetPosts().Count);
            }
        }

        [TestMethod]
        public void Can_Retrieve_Many_Saved_Posts_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    repository.Save(DummyLivePost());
                }

                Assert.AreEqual(4, repository.GetPostsPaged(4, 1).Count);
            }
        }

        [TestMethod]
        public void Paged_Results_Are_Valid()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 15; i++)
                {
                    repository.Save(DummyLivePost());
                }

                Assert.AreEqual(4, repository.GetPostsPaged(4, 1).Count);
                Assert.AreEqual(4, repository.GetPostsPaged(4, 2).Count);
                Assert.AreEqual(4, repository.GetPostsPaged(4, 3).Count);
                Assert.AreEqual(3, repository.GetPostsPaged(4, 4).Count);
            }
        }

        [TestMethod]
        public void Get_Post_By_Id_Check()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();
                repository.Save(post);

                var retrievedPost = repository.GetPostById(post.Id);

                Assert.AreEqual(post, retrievedPost);
            }
        }

        [TestMethod]
        public void Can_Save_Category()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                repository.Save(category);
            }
        }

        [TestMethod]
        public void Can_Load_Saved_Category()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                repository.Save(category);

                Assert.AreEqual(1, repository.GetCategories().Count);
            }
        }

        [TestMethod]
        public void Can_Save_Many_Categories()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {

                for (int i = 0; i < 10; i++)
                {
                    var category = new Category() { Name = "Test" };

                    repository.Save(category);
                }
                
            }
        }

        [TestMethod]
        public void Can_Load_Many_Saved_Categories()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    var category = new Category() { Name = "Test" };

                    repository.Save(category);
                }
                Assert.AreEqual(10, repository.GetCategories().Count);
            }
        }

        [TestMethod]
        public void Can_Load_Many_Saved_Categories_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    var category = new Category() { Name = "Test" };

                    repository.Save(category);
                }
                Assert.AreEqual(4, repository.GetCategoriesPaged(4, 1).Count);
            }
        }

        [TestMethod]
        public void Can_Load_Many_Saved_Categories_Paged_Results_Correctly()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 9; i++)
                {
                    var category = new Category() { Name = String.Format("Test{0}", i) };

                    repository.Save(category);
                }
                Assert.AreEqual(4, repository.GetCategoriesPaged(4, 1).Count);
                Assert.AreEqual(4, repository.GetCategoriesPaged(4, 2).Count);
                Assert.AreEqual(1, repository.GetCategoriesPaged(4, 3).Count);
            }
        }

        [TestMethod]
        public void Can_Save_Post_With_Category_Attached()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                var post = DummyLivePost();

                post.Categories.Add(category);

                repository.Save(post);
                repository.Save(category);

                Assert.AreEqual(1, repository.GetPosts().Count);
                Assert.AreEqual(1, repository.GetCategories().Count);
            }
        }

        [TestMethod]
        public void Can_Persist_Changes()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                repository.Save(post);

                Assert.AreEqual(1, repository.GetPosts().Count);
            }

            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(1, repository.GetPosts().Count);
            }
        }

        [TestMethod]
        public void Can_Persist_Joins()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                var category = new Category() { Name = "Test" };

                post.Categories.Add(category);

                repository.Save(post);
                repository.Save(category);

                Assert.AreEqual(1, repository.GetPosts().Count);
            }

            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(1, repository.GetPosts().Count);
                Assert.AreEqual(1, repository.GetCategories().Count);

                var post = repository.GetPosts().First();
                Assert.AreEqual(1, post.Categories.Count);
            }
        }

        [TestMethod]
        public void Can_Get_Posts_By_Category_Id()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                var category = new Category() { Name = "Test" };

                post.Categories.Add(category);

                repository.Save(post);
                repository.Save(category);

                repository.CommitChanges();

                Assert.AreEqual(post, repository.GetPostsByCategory(category.Id).First());
            }
        }

        [TestMethod]
        public void Can_Get_Posts_By_Category_Id_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                for (int i = 0; i < 10; i++)
                {
                    var post = DummyLivePost();

                    post.Categories.Add(category);

                    repository.Save(post);
                }

                repository.Save(category);

                repository.CommitChanges();

                Assert.AreEqual(4, repository.GetPostsByCategoryPaged(category.Id, 4, 1).Count);
            }
        }

        [TestMethod]
        public void Can_Get_Posts_By_Category_Id_Paged_And_It_Works_Correctly()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                for (int i = 0; i < 17; i++)
                {
                    var post = DummyLivePost();

                    post.Categories.Add(category);

                    repository.Save(post);
                }

                repository.Save(category);

                repository.CommitChanges();

                Assert.AreEqual(5, repository.GetPostsByCategoryPaged(category.Id, 5, 1).Count);
                Assert.AreEqual(5, repository.GetPostsByCategoryPaged(category.Id, 5, 2).Count);
                Assert.AreEqual(5, repository.GetPostsByCategoryPaged(category.Id, 5, 3).Count);
                Assert.AreEqual(2, repository.GetPostsByCategoryPaged(category.Id, 5, 4).Count);
            }
        }

        [TestMethod]
        public void Can_Get_Categories_By_Id()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                repository.Save(category);

                var retreivedCategory = repository.GetCategoryById(category.Id);

                Assert.AreEqual(category, retreivedCategory);
            }
        }

        [TestMethod]
        public void Can_Save_Comment()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comment = new Comment() { Name = "Test Comment", Content = "Test Content" };

                repository.Save(comment);
            }
        }

        [TestMethod]
        public void Can_Retrieve_Saved_Comment()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comment = new Comment() { Name = "Test Comment", Content = "Test Content" };

                repository.Save(comment);

                Assert.AreEqual(1, repository.GetComments().Count);
            }
        }

        [TestMethod]
        public void Can_Retrieve_Saved_Comment_And_It_Is_Correct()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comment = new Comment() { Name = "Test Comment", Content = "Test Content" };

                repository.Save(comment);

                var retrievedComment = repository.GetComments().First();

                Assert.AreEqual(comment, retrievedComment);
            }
        }

        [TestMethod]
        public void Can_Save_Many_Comments()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    repository.Save(new Comment() { Name = String.Format("Test Comment{0}", i), Content = String.Format("Test Content{0}", i) });
                }

                Assert.AreEqual(10, repository.GetComments().Count);
            }
        }

        [TestMethod]
        public void Can_Retrieve_Comments_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    repository.Save(new Comment() { Name = String.Format("Test Comment{0}", i), Content = String.Format("Test Content{0}", i) });
                }

                Assert.AreEqual(4, repository.GetCommentsPaged(4, 1).Count);
            }
        }

        [TestMethod]
        public void Can_Retrieve_Comments_Paged_And_All_Are_Correct()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 11; i++)
                {
                    repository.Save(new Comment() { Name = String.Format("Test Comment{0}", i), Content = String.Format("Test Content{0}", i) });
                }

                Assert.AreEqual(4, repository.GetCommentsPaged(4, 1).Count);
                Assert.AreEqual(4, repository.GetCommentsPaged(4, 2).Count);
                Assert.AreEqual(3, repository.GetCommentsPaged(4, 3).Count);
            }
        }

        [TestMethod]
        public void Can_Associate_Posts_And_Comments()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                var comment = new Comment() { Name = "test", Content = "Test" };

                post.Comments.Add(comment);

                repository.Save(post);
                repository.Save(comment);

                Assert.AreEqual(1, post.Comments.Count);
            }
            using (var repository = new NHibernateBlogService())
            {
                var post = repository.GetPosts().First();

                Assert.AreEqual(1, post.Comments.Count);
            }

        }

        [TestMethod]
        public void Can_Get_Comments_By_Post_Id()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                var comment = new Comment() { Name = "test", Content = "Test" };

                post.Comments.Add(comment);

                repository.Save(post);
                repository.Save(comment);

                var retrievedComment = repository.GetCommentsByPost(post.Id).First();

                Assert.AreEqual(comment, retrievedComment);
            }
        }

        [TestMethod]
        public void Can_Get_Comments_By_Post_Id_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();
                for (int i = 0; i < 10; i++)
                {
                    var comment = new Comment() { Name = String.Format("test{0}", i), Content = String.Format("{0}Test", i) };

                    post.Comments.Add(comment);

                    repository.Save(comment);

                }

                repository.Save(post);

            }

            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(4, repository.GetCommentsByPostPaged(1, 4, 1).Count);
            }
        }

        [TestMethod]
        public void Can_Get_Comments_By_Post_Id_Paged_Accuracy_Check()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();
                for (int i = 0; i < 21; i++)
                {
                    var comment = new Comment() { Name = String.Format("test{0}", i), Content = String.Format("{0}Test", i) };

                    post.Comments.Add(comment);

                    repository.Save(comment);

                }

                repository.Save(post);

                Assert.AreEqual(10, repository.GetCommentsByPostPaged(post.Id, 10, 1).Count);
                Assert.AreEqual(10, repository.GetCommentsByPostPaged(post.Id, 10, 2).Count);
                Assert.AreEqual(1, repository.GetCommentsByPostPaged(post.Id, 10, 3).Count);
            }
        }

        [TestMethod]
        public void Can_Get_Comments_By_Id()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comment = new Comment() { Name = "test", Content = "test" };

                repository.Save(comment);

                var retrievedComment = repository.GetCommentById(comment.Id);

                Assert.AreEqual(comment, retrievedComment);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Will_Fail_Saving_An_Invalid_Post()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = new Post();

                Assert.IsFalse(post.IsValid);

                repository.Save(post);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Will_Fail_Saving_An_Invalid_Comment()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comment = new Comment();

                Assert.IsFalse(comment.IsValid);

                repository.Save(comment);
            }
        }

        [TestMethod]
        public void Can_Delete_Post()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                repository.Save(post);

                Assert.AreEqual(1, repository.GetPosts().Count);

                repository.Delete(post);

                Assert.AreEqual(0, repository.GetPosts().Count);
            }
        }

        [TestMethod]
        public void Can_Delete_Category()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "test" };

                repository.Save(category);

                Assert.AreEqual(1, repository.GetCategories().Count);

                repository.Delete(category);

                Assert.AreEqual(0, repository.GetCategories().Count);
            }
        }

        [TestMethod]
        public void Can_Delete_Comment()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comment = new Comment() { Name = "test", Content = "Test" };

                repository.Save(comment);

                Assert.AreEqual(1, repository.GetComments().Count);

                repository.Delete(comment);

                Assert.AreEqual(0, repository.GetComments().Count);
            }
        }

        [TestMethod]
        public void Comments_Are_Ordered_By_Date_Descending()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var comments = new List<Comment>();
                for (int i = 0; i < 10; i++)
                {
                    var comment = new Comment() { Name = "test", Content = "Test" };

                    comments.Add(comment);

                    Thread.Sleep(1);
                }

                comments.Reverse();
                comments.ForEach(x => repository.Save(x));

                Assert.IsTrue(comments.First().DateCreated > comments.Last().DateCreated);
            }

            using (var repository = new NHibernateBlogService())
            {
                var loadedComments = repository.GetComments();

                Assert.IsFalse(loadedComments.First().DateCreated > loadedComments.Last().DateCreated);
            }
        }

        [TestMethod]
        public void Can_Return_Published_Posts()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    repository.Save(post);
                }

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.IsPublished = false;

                    repository.Save(post);
                }
            }
            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(5, repository.GetPublishedPosts().Count);
            }
        }

        [TestMethod]
        public void Can_Return_Published_Posts_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    repository.Save(post);
                }

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.IsPublished = false;

                    repository.Save(post);
                }
            }
            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(2, repository.GetPublishedPostsPaged(2, 1).Count);
            }
        }

        [TestMethod]
        public void Can_Return_Published_Post_By_Id()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var post = DummyLivePost();

                repository.Save(post);
            }
            using (var repository = new NHibernateBlogService())
            {
                Assert.IsNotNull(repository.GetPublishedPostById(1));
            }
        }

        [TestMethod]
        public void Can_Return_Published_Posts_By_Category()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.Categories.Add(category);

                    repository.Save(post);
                }

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.IsPublished = false;
                    post.Categories.Add(category);

                    repository.Save(post);
                }
                repository.Save(category);
            }
            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(10, repository.GetPostsByCategory(1).Count);
                Assert.AreEqual(5, repository.GetPublishedPostsByCategory(1).Count);
            }
        }

        [TestMethod]
        public void Can_Return_Published_Posts_By_Category_Paged()
        {
            Initialize(true);

            using (var repository = new NHibernateBlogService())
            {
                var category = new Category() { Name = "Test" };

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.Categories.Add(category);

                    repository.Save(post);
                }

                for (int i = 0; i < 5; i++)
                {
                    var post = DummyLivePost();

                    post.IsPublished = false;
                    post.Categories.Add(category);

                    repository.Save(post);
                }
                repository.Save(category);
            }
            using (var repository = new NHibernateBlogService())
            {
                Assert.AreEqual(2, repository.GetPublishedPostsByCategoryPaged(1, 2, 1).Count);
            }
        }

    }
}
