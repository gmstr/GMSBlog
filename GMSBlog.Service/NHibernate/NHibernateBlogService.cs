using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Entities;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using GMSBlog.Service.NHibernate.Mappings;
using NHibernate.LambdaExtensions;
using GMSBlog.Service.NHibernate.Helpers;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace GMSBlog.Service.NHibernate
{
    public class NHibernateBlogService : IBlogService
    {
        public NHibernateBlogService() : this(true) { }

        public NHibernateBlogService(bool autoCommit)
        {
            AutoCommit = autoCommit;

            _session = SessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        private ISession _session;

        private ITransaction _transaction;

        static ISessionFactory SessionFactory = CreateSessionFactory();
        
        private static ISessionFactory CreateSessionFactory()
        {
            return CreateSessionFactory(ConfigurationManager.AppSettings["BlogConnectionStringName"]);
        }

        private static ISessionFactory CreateSessionFactory(string connStringName)
        {
            var cfg = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008
                        .ConnectionString(c => c.FromConnectionStringWithKey(connStringName)))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMappings>());

            return cfg.BuildSessionFactory();
        }
        #region IBlogService Members

        public bool AutoCommit { get; set; }

        private ICriteria getPosts()
        {
            return _session.CreateCriteria<Post>();
        }

        private ICriteria getComments()
        {
            return _session.CreateCriteria<Comment>().AddOrder(Order.Asc("DateCreated"));
        }

        private ICriteria getCategories()
        {
            return _session.CreateCriteria<Category>();
        }

        public IList<Post> GetPosts()
        {
            return getPosts().List<Post>();
        }

        public IList<Post> GetPostsPaged(int pageSize, int page)
        {
            return getPosts().SetPages(pageSize, page).List<Post>();
        }

        public Post GetPostById(int id)
        {
            return getPosts().Add<Post>(x => x.Id == id)
                .SetFetchMode<Post>(x => x.Comments, FetchMode.Eager)
                .UniqueResult<Post>();
        }

        public IList<Post> GetPostsByCategory(int categoryId)
        {
            return getPosts().CreateCriteria<Post>(x => x.Categories).Add<Category>(x => x.Id == categoryId).List<Post>();
        }

        public IList<Post> GetPostsByCategoryPaged(int categoryId, int pageSize, int page)
        {
            return getPosts().CreateCriteria<Post>(x => x.Categories).Add<Category>(x => x.Id == categoryId).SetPages(pageSize, page).List<Post>();
        }

        public IList<Category> GetCategories()
        {
            return getCategories().List<Category>();
        }

        public IList<Category> GetCategoriesPaged(int pageSize, int page)
        {
            return getCategories().SetPages(pageSize, page).List<Category>();
        }

        public Category GetCategoryById(int id)
        {
            return getCategories().Add<Category>(x => x.Id == id).UniqueResult<Category>();
        }

        public IList<Comment> GetComments()
        {
            return getComments().List<Comment>();
        }

        public IList<Comment> GetCommentsPaged(int pageSize, int page)
        {
            return getComments().SetPages(pageSize, page).List<Comment>();
        }

        public IList<Comment> GetCommentsByPost(int postId)
        {
            return getComments().Add<Comment>(comm => comm.Post.Id == postId).List<Comment>();
        }

        public IList<Comment> GetCommentsByPostPaged(int postId, int pageSize, int page)
        {
            return getComments().Add<Comment>(comm => comm.Post.Id == postId).SetPages(pageSize, page).List<Comment>();
        }

        public Comment GetCommentById(int id)
        {
            return getComments().Add<Comment>(comm => comm.Id == id).UniqueResult<Comment>();
        }

        private void save(object blogObject)
        {
            _session.SaveOrUpdate(blogObject);
        }
        public void Save(Post post)
        {
            if (!post.IsValid)
            {
                var errorParser = new StringBuilder();
                post.RuleViolations.ToList().ForEach(vio =>
                {
                    errorParser.AppendLine(String.Format("- {0}: {1}", vio.Property, vio.Violation));
                });
                throw new InvalidOperationException(String.Format("Cannot save an invalid post.{0}{0} Current Violations: {0}{1}", Environment.NewLine, errorParser));
            }
            save(post);
        }

        public void Save(Category category)
        {
            save(category);
        }

        public void Save(Comment comment)
        {
            if (!comment.IsValid)
            {
                var errorParser = new StringBuilder();
                comment.RuleViolations.ToList().ForEach(vio =>
                {
                    errorParser.AppendLine(String.Format("- {0}: {1}", vio.Property, vio.Violation));
                });
                throw new InvalidOperationException(String.Format("Cannot save an invalid comment.{0}{0} Current Violations: {0}{1}", Environment.NewLine, errorParser));
            }
            save(comment);
        }

        public void Delete(Post post)
        {
            _session.Delete(post);
        }

        public void Delete(Category category)
        {
            _session.Delete(category);
        }

        public void Delete(Comment comment)
        {
            _session.Delete(comment);
        }

        public void CommitChanges()
        {
            _transaction.Commit();
        }


        public void Dispose()
        {
            try
            {
                if (AutoCommit && !_transaction.WasCommitted)
                {
                    _transaction.Commit();
                }
                else if (!_transaction.WasCommitted)
                {
                    _transaction.Rollback();
                }
            }
            finally
            {
                if (_session.IsOpen)
                {
                    _session.Close();
                    _session.Dispose();
                }
            }
        }

        #endregion

        #region IBlogService Members


        public IList<Post> GetPublishedPosts()
        {
            return getPosts().Add<Post>(x => x.IsPublished)
                .SetFetchMode<Post>(x => x.Comments, FetchMode.Eager).AddOrder(Order.Desc("DateCreated")).List<Post>();
        }

        public IList<Post> GetPublishedPostsPaged(int pageSize, int page)
        {
            return getPosts().Add<Post>(x => x.IsPublished)
                .SetFetchMode<Post>(x => x.Comments, FetchMode.Eager).AddOrder(Order.Desc("DateCreated")).SetPages(pageSize, page).List<Post>();
        }

        public Post GetPublishedPostById(int id)
        {
            return getPosts().Add<Post>(x => x.Id == id).Add<Post>(x => x.IsPublished)
                .SetFetchMode<Post>(x => x.Comments, FetchMode.Eager).UniqueResult<Post>();
        }

        public IList<Post> GetPublishedPostsByCategory(int categoryId)
        {
            return getPosts().Add<Post>(x => x.IsPublished)
                .SetFetchMode<Post>(x => x.Comments, FetchMode.Eager).AddOrder(Order.Desc("DateCreated")).CreateCriteria<Post>(x => x.Categories).Add<Category>(x => x.Id == categoryId).List<Post>();
        }

        public IList<Post> GetPublishedPostsByCategoryPaged(int categoryId, int pageSize, int page)
        {
            return getPosts().Add<Post>(x => x.IsPublished)
                .SetFetchMode<Post>(x => x.Comments, FetchMode.Eager).AddOrder(Order.Desc("DateCreated")).CreateCriteria<Post>(x => x.Categories).Add<Category>(x => x.Id == categoryId).SetPages(pageSize, page).List<Post>();
        }


        /*
         * public IList<Post> GetPosts()
        {
            return getPosts().List<Post>();
        }

        public IList<Post> GetPostsPaged(int pageSize, int page)
        {
            return getPosts().SetPages(pageSize, page).List<Post>();
        }

        public Post GetPostById(int id)
        {
            return getPosts().Add<Post>(x => x.Id == id).UniqueResult<Post>();
        }

        public IList<Post> GetPostsByCategory(int categoryId)
        {
            return getPosts().CreateCriteria<Post>(x => x.Categories).Add<Category>(x => x.Id == categoryId).List<Post>();
        }

        public IList<Post> GetPostsByCategoryPaged(int categoryId, int pageSize, int page)
        {
            return getPosts().CreateCriteria<Post>(x => x.Categories).Add<Category>(x => x.Id == categoryId).SetPages(pageSize, page).List<Post>();
        }
         * */
        #endregion
    }
}