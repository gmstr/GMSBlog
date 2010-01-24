using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Entities;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;

namespace GMSBlog.Service.NHibernate.Mappings
{
    public class PostMappings: ClassMap<Post>
    {
        public PostMappings()
        {
            Table("Posts");

            Id(x => x.Id).Unique().GeneratedBy.Native().Not.Nullable();
            Map(x => x.Title).Not.Nullable().Length(250);
            Map(x => x.Summary).Not.Nullable().Length(500);
            Map(x => x.Content).Not.Nullable().CustomSqlType("text");
            Map(x => x.DateCreated).Not.Nullable();
            Map(x => x.DateUpdated).Not.Nullable();
            Map(x => x.IsPublished).Not.Nullable();
            
            HasMany(x => x.Comments)
                .Table("Comments").Cascade.SaveUpdate();

            HasManyToMany(x => x.Categories)
                .AsBag()
                .Table("PostCategories")
                .ParentKeyColumn("PostId")
                .ChildKeyColumn("CategoryId").Cascade.SaveUpdate();
        }
    }
}
