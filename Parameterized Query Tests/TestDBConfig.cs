using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameterized_Query_Tests
{
    public class TestDBConfig :DbConfiguration
    {
        public TestDBConfig()
            {
                SetDefaultConnectionFactory(new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", "", @"Data Source=|DataDirectory|\test.sdf"));
                SetDatabaseInitializer<PersonContext>(new TestDBInitializer());
            }


        
    }

    public class TestDBInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        protected override void Seed(PersonContext context)
        {
            
            context.People.Add(new Person()
            {
                PersonID = 1,
                FirstName = "John",
                LastName = "James",
                Children = 5,
                DateOfBirth = DateTime.Parse("1/5/1970")
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
