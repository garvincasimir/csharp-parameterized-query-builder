using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Common;


namespace Parameterized_Query_Tests
{
    public class TestDBConfig : DbConfiguration
    {
        public TestDBConfig()
        {
            #if __MonoCS__
			    base.AddDependencyResolver (new MySqlDependencyResolver ());
			    base.SetProviderFactory (MySqlProviderInvariantName.ProviderName, new MySqlClientFactory ());
			    base.SetProviderServices (MySqlProviderInvariantName.ProviderName, new MySqlProviderServices ());
			    base.SetDefaultConnectionFactory (new MySqlConnectionFactory ());
			    base.SetMigrationSqlGenerator (MySqlProviderInvariantName.ProviderName, () => new MySqlMigrationSqlGenerator());
			    base.SetProviderFactoryResolver (new MySqlProviderFactoryResolver ());
			    base.SetManifestTokenResolver (new MySqlManifestTokenResolver ());
            #else
                base.SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
            #endif

            SetDatabaseInitializer<PersonContext>(new TestDBInitializer());

        }



    }

    public class TestDBInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        protected override void Seed(PersonContext context)
        {

            context.People.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
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
