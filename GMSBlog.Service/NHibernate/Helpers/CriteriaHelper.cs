using System;
using NHibernate;

namespace GMSBlog.Service.NHibernate.Helpers
{
    public static class CriteriaHelper
    {
        public static ICriteria SetPages(this ICriteria criteria, int pageSize, int page)
        {
            return criteria.SetMaxResults(pageSize).SetFirstResult((page - 1) * pageSize);
        }
    }
}
