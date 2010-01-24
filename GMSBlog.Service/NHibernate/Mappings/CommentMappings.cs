using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Entities;
using FluentNHibernate.Mapping;

namespace GMSBlog.Service.NHibernate.Mappings
{
    public class CommentMappings : ClassMap<Comment>
    {
        public CommentMappings()
        {
            Id(x => x.Id).Unique().GeneratedBy.Native().Not.Nullable();
            Map(x => x.Content).Not.Nullable().CustomSqlType("text");
            Map(x => x.Name).Not.Nullable().Length(250);
            Map(x => x.Website).Nullable().Length(500);
            Map(x => x.DateCreated).Not.Nullable();

            References(x => x.Post).Class<Post>().Cascade.SaveUpdate();
        }
    }
}
