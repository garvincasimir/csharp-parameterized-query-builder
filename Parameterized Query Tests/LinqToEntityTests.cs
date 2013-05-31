using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parameterized_Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;

namespace Parameterized_Query_Tests
{
    [TestClass]
    public class LinqToEntityTests
    {
        PersonContext context;
        string dbPath;

        [TestInitialize]
        public void Setup()
        {
            // file path of the database to create
            dbPath = Path.GetTempFileName();
            Console.WriteLine(dbPath);
            // delete it if it already exists
            if (File.Exists(dbPath))
                File.Delete(dbPath);

            // create the SQL CE connection string - this just points to the file path
            string connectionString = "Datasource = " + dbPath;

            // NEED TO SET THIS TO MAKE DATABASE CREATION WORK WITH SQL CE!!!
            Database.DefaultConnectionFactory =
                new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            context = new PersonContext(connectionString);
          
                // this will create the database with the schema from the Entity Model
            context.Database.Create();

            context.People.Add(new Person()
            {
                PersonID = 1,
                FirstName = "John",
                LastName = "James",
                Children = 5,
                DateOfBirth = DateTime.Parse("1/5/1970")
            });

            context.SaveChanges();
   
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(dbPath);
        }

        [TestMethod]
        public void TestStringSingleParamEquals()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "FirstName",
                    SearchType = ComparisonType.Equals,
                    SearchValue = "John"

                }
            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 1);

        }

        [TestMethod]
        public void TestStringSingleParamEqualsWithNoMatch()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "FirstName",
                    SearchType = ComparisonType.Equals,
                    SearchValue = "Johnzzz"

                }
            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 0);

        }

        [TestMethod]
        public void TestStringMultipleParamEquals()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "FirstName",
                    SearchType = ComparisonType.Equals,
                    SearchValue = "John"
                },
                new SearchParameter
                {
                    FieldName = "LastName",
                    SearchType = ComparisonType.Equals,
                    SearchValue = "James"
                }

            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 1);
        }

        [TestMethod]
        public void TestDateSingleParamEquals()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "DateOfBirth",
                    SearchType = ComparisonType.Equals,
                    SearchValue =DateTime.Parse("1/5/1970")
                }

            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 1);
        }

        [TestMethod]
        public void TestDateSingleParamEqualsWithNoMatch()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "DateOfBirth",
                    SearchType = ComparisonType.Equals,
                    SearchValue =DateTime.Parse("1/5/1971")
                }

            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 0);
        }

        [TestMethod]
        public void TestDateSingleParamBetween()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "DateOfBirth",
                    SearchType = ComparisonType.Between,
                    SearchValue =DateTime.Parse("1/1/1970"),
                    SearchValue2 = DateTime.Parse("1/30/1970")
                }

            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 1);
        }

        [TestMethod]
        public void TestDateSingleParamBetweenNoMatch()
        {
            var searchParams = new List<SearchParameter>
            {
                new SearchParameter
                {
                    FieldName = "DateOfBirth",
                    SearchType = ComparisonType.Between,
                    SearchValue =DateTime.Parse("1/30/1970"),
                    SearchValue2 = DateTime.Parse("3/30/1970")
                }

            };

            var pq = new ParameterizedQuery<Person>(context.People, searchParams);

            Assert.IsTrue(pq.Results().Count() == 0);
        }
    }
}
