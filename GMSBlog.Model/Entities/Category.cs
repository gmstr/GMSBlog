using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMSBlog.Model.Entities
{
    public class Category
    {
        public Category()
        {
            Posts = new List<Post>();
        }

        private int _id;
        public virtual int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private string _name;
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private IList<Post> _posts;
        public virtual IList<Post> Posts
        {
            get
            {
                return _posts;
            }
            set
            {
                _posts = value;
            }
        }

        public virtual IEnumerable<Post> PublishedPosts
        {

            get
            {
                return _posts.Where(x => x.IsPublished);
            }
        }

        public virtual void AddPost(Post post)
        {
            Posts.Add(post);
            post.Categories.Add(this);
        }

        public virtual void RemovePost(Post post)
        {
            Posts.Remove(post);
            post.Categories.Remove(this);
        }
    }
}
