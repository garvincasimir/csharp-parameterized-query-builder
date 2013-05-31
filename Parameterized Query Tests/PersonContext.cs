using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameterized_Query_Tests
{
    public partial class PersonContext : DbContext
    {
        public PersonContext()
            : base("name=PersonContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public PersonContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Person> People { get; set; }

    }
}
