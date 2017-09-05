using Clearsoft.BoxOffice.Data.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsoft.BoxOffice.Data.SqlServer.Mapping
{
    public class EventMap : VersionedClassMap<Event>
    {
        public EventMap()
        {
            Id(x => x.EventId);
            Map(x => x.Name).Not.Nullable();
            
            Map(x => x.CreatedDate).Not.Nullable();

            References(x => x.Status,"StatusId");
            References(x => x.CreatedBy, "CreatedUserId");

            HasManyToMany(x => x.Performances).Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore).Table("EventPerformance").ParentKeyColumn("EventId").ChildKeyColumn("PerformanceId");
        }
    }
}
