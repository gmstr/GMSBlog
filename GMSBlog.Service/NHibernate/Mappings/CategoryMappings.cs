using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSBlog.Model.Entities;
using FluentNHibernate.Mapping;

namespace GMSBlog.Service.NHibernate.Mappings
{
    public class CategoryMappings : ClassMap<Category>
    {
        public CategoryMappings()
        {
            Table("Categories");

            Id(x => x.Id).Unique().GeneratedBy.Native().Not.Nullable();
            Map(x => x.Name).Not.Nullable().Length(500);

            HasManyToMany(x => x.Posts)
                .AsBag()
                .Table("PostCategories")
                .ParentKeyColumn("CategoryId")
                .ChildKeyColumn("PostId").Cascade.SaveUpdate().Inverse();

        }
    }
}
