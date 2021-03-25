using CsvEnt.Bind;
using CsvEnt.Tests.ExpectedData;
using CsvEnt.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CsvEnt.Tests
{
    [TestClass]
    public class BindTest
    {
        [TestMethod]
        public void FullBindTest()
        {
            var entities = new CsvBinder<TestItem>()
                .AddRule(0, e => e.Int, BindMappers.Int)
                .AddRule(1, e => e.NullInt, BindMappers.NullInt)
                .AddRule(2, e => e.Double, BindMappers.Double)
                .AddRule(3, e => e.NullDouble, BindMappers.NullDouble)
                .AddRule(4, e => e.Bool, (value) => BindMappers.Bool(value, "True"))
                .AddRule(5, e => e.NullBool, (value) => BindMappers.NullBool(value, "True"))
                .AddRule(6, e => e.Date, (value) => BindMappers.Date(value, "dd.MM.yyyy"))
                .AddRule(7, e => e.NullDate, (value) => BindMappers.NullDate(value, "dd.MM.yyyy"))
                .AddRule(8, e => e.String, BindMappers.String)
                .AddRule(9, e => e.Enum, BindMappers.ValueEnum<TestEnum>)
                .Skip(1)
                .Bind(Expected.TestItemsDataFile);

            CollectionAssert.AreEqual(entities, Expected.Data);
        }

        [TestMethod]
        public void PartBindTest()
        {
            var expected = Expected.Data
                .Take(1)
                .ToArray();
            var entities = new CsvBinder<TestItem>()
                .AddRule(0, e => e.Int, BindMappers.Int)
                .AddRule(1, e => e.NullInt, BindMappers.NullInt)
                .AddRule(2, e => e.Double, BindMappers.Double)
                .AddRule(3, e => e.NullDouble, BindMappers.NullDouble)
                .AddRule(4, e => e.Bool, (value) => BindMappers.Bool(value, "True"))
                .AddRule(5, e => e.NullBool, (value) => BindMappers.NullBool(value, "True"))
                .AddRule(6, e => e.Date, (value) => BindMappers.Date(value, "dd.MM.yyyy"))
                .AddRule(7, e => e.NullDate, (value) => BindMappers.NullDate(value, "dd.MM.yyyy"))
                .AddRule(8, e => e.String, BindMappers.String)
                .AddRule(9, e => e.Enum, BindMappers.ValueEnum<TestEnum>)
                .Skip(1)
                .Take(1)
                .Bind(Expected.TestItemsDataFile);

            CollectionAssert.AreEqual(entities, expected);
        }

        [TestMethod]
        public void ErrBindTest()
        {
            var binder = new CsvBinder<TestItem>()
                .AddRule(0, e => e.Int, BindMappers.Int)
                // Csv with null column -----------------|
                .AddRule(1, e => e.NullInt, BindMappers.Int)
                .AddRule(2, e => e.Double, BindMappers.Double)
                .AddRule(3, e => e.NullDouble, BindMappers.NullDouble)
                .AddRule(4, e => e.Bool, (value) => BindMappers.Bool(value, "True"))
                .AddRule(5, e => e.NullBool, (value) => BindMappers.NullBool(value, "True"))
                .AddRule(6, e => e.Date, (value) => BindMappers.Date(value, "dd.MM.yyyy"))
                .AddRule(7, e => e.NullDate, (value) => BindMappers.NullDate(value, "dd.MM.yyyy"))
                .AddRule(8, e => e.String, BindMappers.String)
                .AddRule(9, e => e.Enum, BindMappers.ValueEnum<TestEnum>)
                .ContinueOrError(true)
                .Skip(1);

            var entities = binder.Bind(Expected.TestItemsDataFile);

            Assert.IsTrue(entities.Length == 2);
            Assert.IsTrue(binder.Errors.Count == 2);
        }
    }
}
