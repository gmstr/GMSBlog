using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GMSBlog.Service.NHibernate.Mappings;
using NHibernate.Tool.hbm2ddl;
using GMSBlog.Web.Controllers;
using GMSBlog.Web.Models;
using GMSBlog.Service.NHibernate;
using GMSBlog.Model.Entities;
using StructureMap;
using GMSBlog.Web.Support;
using GMSBlog.Web.Tests.Helpers;

namespace GMSBlog.Web.Tests.Controllers
{
    /// <summary>
    /// Summary description for BaseBlogController
    /// </summary>
    [TestClass]
    public class BaseBlogControllerTests
    {
        [TestMethod]
        public void Can_Initialize_SessionFactory()
        {
            DatabaseHelpers.Initialize();
        }

        [TestMethod]
        public void Can_Initialize_SessionFactory_And_Empty()
        {
            DatabaseHelpers.Initialize(true);
        }

        [TestMethod]
        public void Can_Initialize_A_Concrete_Implementation_Of_BaseBlogController()
        {
            BaseBlogController controller = new HomeController() as BaseBlogController;

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void BaseBlogController_Contains_Categories_ViewData()
        {
            BaseBlogController controller = new HomeController() as BaseBlogController;

            Assert.IsNotNull(controller.ViewData["Categories"]);
        }

        [TestMethod]
        public void Categories_ViewData_Is_Of_Type_IList_CategorySummary()
        {
            BaseBlogController controller = new HomeController() as BaseBlogController;

            Assert.IsInstanceOfType(controller.ViewData["Categories"], typeof(IList<CategorySummary>));
        }

        [TestMethod]
        public void Categories_ViewData_Retreives_Live_Categories()
        {
            //Arrange
            DatabaseHelpers.Initialize(true);

            ObjectFactory.Initialize(x =>
            {
                x.UseDefaultStructureMapConfigFile = false;
                x.AddRegistry(new GMSBlogRegistry());
            });

            //Act
            using (var repository = new NHibernateBlogService())
            {
                for (int i = 0; i < 10; i++)
                {
                    repository.Save(new Category() { Name = String.Format("Test{0}", i) });
                }
            }


            BaseBlogController controller = new HomeController() as BaseBlogController;

            //Assert
            Assert.AreEqual(10, (controller.ViewData["Categories"] as IList<CategorySummary>).Count);
        }

        [TestMethod]
        public void Categories_ViewData_Retreives_Live_Categories_In_Order_Of_No_Of_Posts_Descending()
        {
            //Arrange
            DatabaseHelpers.Initialize(true);

            ObjectFactory.Initialize(x =>
            {
                x.UseDefaultStructureMapConfigFile = false;
                x.AddRegistry(new GMSBlogRegistry());
            });

            //Act
            using (var repository = new NHibernateBlogService())
            {

                for (int i = 1; i <= 10; i++)
                {
                    var category = new Category() { Name = String.Format("Test {0}", i) };

                    for (int j = 0; j < i; j++)
                    {
                        var post = new Post()
                        {
                            Content = "Test",
                            IsPublished = true,
                            Keywords = "",
                            Title = "Test",
                            Summary = "Test"
                        };

                        post.Categories.Add(category);

                        repository.Save(post);
                    }

                    repository.Save(category);
                }
            }

            BaseBlogController controller = new HomeController() as BaseBlogController;

            //Assert
            Assert.AreEqual(10, (controller.ViewData["Categories"] as IList<CategorySummary>).First().NoOfPosts);
            Assert.AreEqual(1, (controller.ViewData["Categories"] as IList<CategorySummary>).Last().NoOfPosts);
        }

        [TestMethod]
        public void Categories_ViewData_Retreives_Live_Categories_In_Order_Of_No_Of_Posts_Descending_Not_Counting_UnPublished()
        {
            //Arrange
            DatabaseHelpers.Initialize(true);

            ObjectFactory.Initialize(x =>
            {
                x.UseDefaultStructureMapConfigFile = false;
                x.AddRegistry(new GMSBlogRegistry());
            });

            //Act
            using (var repository = new NHibernateBlogService())
            {

                for (int i = 1; i <= 10; i++)
                {
                    var category = new Category() { Name = String.Format("Test {0}", i) };

                    for (int j = 0; j < i; j++)
                    {
                        var post = new Post()
                        {
                            Content = "Test",
                            IsPublished = j % 2 == 0,
                            Keywords = "",
                            Title = "Test",
                            Summary = "Test"
                        };

                        post.Categories.Add(category);

                        repository.Save(post);
                    }

                    repository.Save(category);
                }
            }

            BaseBlogController controller = new HomeController() as BaseBlogController;

            //Assert
            Assert.AreEqual(5, (controller.ViewData["Categories"] as IList<CategorySummary>).First().NoOfPosts);
            Assert.AreEqual(1, (controller.ViewData["Categories"] as IList<CategorySummary>).Last().NoOfPosts);
        }
    }
}