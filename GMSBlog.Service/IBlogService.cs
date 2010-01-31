using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Entities;

namespace GMSBlog.Service
{
    public interface IBlogService : IDisposable
    {
        bool AutoCommit { get; set; }

        IList<Post> GetPosts();

        IList<Post> GetPostsPaged(int pageSize, int page);

        Post GetPostById(int id);

        Post GetPostByTitleAndDate(string title, DateTime date);

        IList<Post> GetPostsByCategory(int categoryId);

        IList<Post> GetPostsByCategoryPaged(int categoryId, int pageSize, int page);

        IList<Post> GetPublishedPosts();

        IList<Post> GetPublishedPostsPaged(int pageSize, int page);

        Post GetPublishedPostById(int id);

        Post GetPublishedPostByTitleAndDate(string title, DateTime date);

        IList<Post> GetPublishedPostsByCategory(int categoryId);

        IList<Post> GetPublishedPostsByCategoryPaged(int categoryId, int pageSize, int page);

        IList<Category> GetCategories();

        IList<Category> GetCategoriesPaged(int pageSize, int page);

        Category GetCategoryById(int id);

        IList<Comment> GetComments();

        IList<Comment> GetCommentsPaged(int pageSize, int page);

        IList<Comment> GetCommentsByPost(int postId);

        IList<Comment> GetCommentsByPostPaged(int postId, int pageSize, int page);

        Comment GetCommentById(int id);

        void Save(Post post);

        void Save(Category category);

        void Save(Comment comment);

        void Delete(Post post);

        void Delete(Category category);

        void Delete(Comment comment);

        void CommitChanges();
    }
}
