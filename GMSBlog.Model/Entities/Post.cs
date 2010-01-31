using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Validation;
using GMSBlog.Model.PropertyChanged;

namespace GMSBlog.Model.Entities
{
    public class Post : IValidated, IPropertyChanged
    {
        public Post()
        {
            var now = DateTime.Now;
            _dateCreated = now;
            _dateUpdated = now;

            Categories = new List<Category>();
            Comments = new List<Comment>();
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
        private string _title;
        public virtual string Title
        {
            get
            {
                return _title;
            }
            set
            {
                OnPropertyChanged("Title");
                _title = value;
            }
        }
        private string _summary;
        public virtual string Summary
        {
            get
            {
                return _summary;
            }
            set
            {
                OnPropertyChanged("Summary");
                _summary = value;
            }
        }
        private string _content;
        public virtual string Content
        {
            get
            {
                return _content;
            }
            set
            {
                OnPropertyChanged("Content");
                _content = value;
            }
        }
        private string _keywords;
        public virtual string Keywords
        {
            get
            {
                return _keywords;
            }
            set
            {
                OnPropertyChanged("Keywords");
                _keywords = value;
            }
        }
        private DateTime _dateCreated;
        public virtual DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            private set
            {
                _dateCreated = value;
            }
        }
        private DateTime _dateUpdated;
        public virtual DateTime DateUpdated
        {
            get
            {
                return _dateUpdated;
            }
            private set
            {
                _dateUpdated = value;
            }
        }
        private bool _isPublished;
        public virtual bool IsPublished
        {
            get
            {
                return IsValid ? _isPublished : false;
            }
            set
            {
                OnPropertyChanged("IsPublished");
                _isPublished = value;
            }
        }

        private IList<Category> _categories;
        public virtual IList<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
            }
        }

        private IList<Comment> _comments;
        public virtual IList<Comment> Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value; 
            }
        }
        public virtual void AddCategory(Category category)
        {
            Categories.Add(category);
            category.Posts.Add(this);
        }

        public virtual void RemoveCategory(Category category)
        {
            Categories.Remove(category);
            category.Posts.Remove(this);
        }

        public virtual void AddComment(Comment comment)
        {
            Comments.Add(comment);
            comment.SetPost(this);
        }

        public virtual void RemoveComment(Comment comment)
        {
            Comments.Remove(comment);
            comment.SetPost(null);
        }
        #region IValidated Members

        public virtual bool IsValid
        {
            get { return !RuleViolations.Any(); }
        }

        public virtual IEnumerable<RuleViolation> RuleViolations
        {
            get
            {
                if (string.IsNullOrEmpty(Title)) { yield return new RuleViolation("Title", "There must be a title specified for the post"); }
                if (string.IsNullOrEmpty(Summary)) { yield return new RuleViolation("Summary", "There must be a summary specified for the post"); }
                if (string.IsNullOrEmpty(Content)) { yield return new RuleViolation("Content", "There must be content specified for the post"); }
                yield break;
            }
        }

        #endregion

        #region IPropertyChanged Members

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "Title" || propertyName == "Summary" || propertyName == "Content") { _dateUpdated = DateTime.Now; }
        }

        #endregion
    }
}
