using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NHibernate_Tutorial.Mappings
{
    public class EmployeeMapping : ClassMap<Model.Employee>
    {
        public EmployeeMapping()
        {
            Id(x => x.Id);
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Table("Employee");
        }
    }
}
