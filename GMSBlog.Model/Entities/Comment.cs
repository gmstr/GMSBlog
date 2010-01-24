using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Validation;
using System.Net;
using System.Net.NetworkInformation;

namespace GMSBlog.Model.Entities
{
    public class Comment: IValidated
    {
        public Comment()
        {
            DateCreated = DateTime.Now;
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
        private string _website;
        public virtual string Website
        {
            get
            {
                return _website;
            }
            set
            {
                _website = value;
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
                _content = value;
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

        private Post _post;
        public virtual Post Post
        {
            get
            {
                return _post;
            }
            set
            {
                _post = value;
            }
        }

        public virtual void SetPost(Post post)
        {
            if (post != null)
            {
                if (!post.Comments.Contains(this))
                {
                    post.Comments.Add(this);
                }
            }
            else { _post.Comments.Remove(this); }
            _post = post;

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
                if (string.IsNullOrEmpty(Name)) { yield return new RuleViolation("Name", "You must supply a name for your comment"); }
                if (string.IsNullOrEmpty(Content)) { yield return new RuleViolation("Content", "You must provide content for your comment"); }

                if (!string.IsNullOrEmpty(Website))
                {
                    var websiteIsValid = true;
                    try
                    {
                        var uri = new Uri(Website);
                        if (!(uri.Scheme == "http" || uri.Scheme == "https"))
                        {
                            websiteIsValid = false;
                        }
                    }
                    catch { websiteIsValid = false; }
                    if (!websiteIsValid) { yield return new RuleViolation("Website", "The website you have provided was not in a valid format, please check that you have included the valid http:// or https:// prefix"); }
                }

                yield break;
            }
        }

        #endregion
    }

}
