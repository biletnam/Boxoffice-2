using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clearsoft.BoxOffice.Data.Entities;
using FluentNHibernate.Mapping;

namespace Clearsoft.BoxOffice.Data.SqlServer.Mapping
{
    public abstract class VersionedClassMap<T> : ClassMap<T> where T : IVersionedEntity
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version).Column("ts").CustomSqlType("Rowversion").Generated.Always().UnsavedValue("null");
        }

    }
}
