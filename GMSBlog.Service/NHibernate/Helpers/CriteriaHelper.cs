using System;
using NHibernate;
using NHibernate.Transform;
using NHibernate.LambdaExtensions;
using GMSBlog.Model.Entities;

namespace GMSBlog.Service.NHibernate.Helpers
{
    public static class CriteriaHelper
    {
        public static ICriteria SetPages(this ICriteria criteria, int pageSize, int page)
        {
            return criteria.SetMaxResults(pageSize).SetFirstResult((page - 1) * pageSize);
        }

        public static ICriteria FetchComments(this ICriteria criteria)
        {
            return criteria.SetFetchMode<Post>(x => x.Comments, FetchMode.Eager).SetResultTransformer(new DistinctRootEntityResultTransformer());
        }       
    }
}
