using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameterized_Query_Tests
{
    [DbConfigurationType(typeof(TestDBConfig))]
    public partial class PersonContext : DbContext
    {
        public PersonContext(){}
        public PersonContext(string connectionString): base(connectionString){ }
        public DbSet<Person> People { get; set; }
    }
}
