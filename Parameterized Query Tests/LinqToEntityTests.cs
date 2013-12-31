using NUnit.Framework;
using Parameterized_Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;

namespace Parameterized_Query_Tests
{
    [TestFixture]
    public class LinqToEntityTests
    {
        PersonContext context;

        [SetUp]
        public void setup()
        {

            #if __MonoCS__
			    context = new PersonContext("server=127.0.0.1;database=mono;User Id=travis;");
            #else
                context = new PersonContext();
            #endif

        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
