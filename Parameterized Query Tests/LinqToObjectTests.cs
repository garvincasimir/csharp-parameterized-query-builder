using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parameterized_Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parameterized_Query_Tests
{
    [TestClass]
    public class LinqToObjectTests
    {
        List<MockPerson> people;

        [TestInitialize]
        public void Setup()
        {
            people = new List<MockPerson>();

            people.Add(new MockPerson()
            {
                PersonID = 1,
                FirstName = "John",
                LastName = "James",
                Children = 5,
                DateOfBirth = DateTime.Parse("1/5/1970")
                
            });
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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

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

            var pq = new ParameterizedQuery<MockPerson>(people.AsQueryable(), searchParams);

            Assert.IsTrue(pq.Results().Count() == 0);
        }
    }
}
