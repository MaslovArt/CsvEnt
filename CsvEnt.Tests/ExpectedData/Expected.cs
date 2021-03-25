using CsvEnt.Tests.Models;
using System;

namespace CsvEnt.Tests.ExpectedData
{
    public static class Expected
    {
        public static string TestItemsDataFile => @"ExpectedData\TestItems.csv";

        public static TestItem[] Data => new TestItem[]
        {
            new TestItem()
            {
                Int = 1,
                NullInt = 1,
                Double = 2.5,
                NullDouble = 2.5,
                Date = new DateTime(2020, 2, 20),
                NullDate = new DateTime(2020, 2, 20),
                Bool = true,
                NullBool = false,
                String = "abc",
                Enum = TestEnum.Value1
            },
            new TestItem()
            {
                Int = 1,
                NullInt = null,
                Double = 2.5,
                NullDouble = null,
                Date = new DateTime(2020, 2, 20),
                NullDate = null,
                Bool = true,
                NullBool = null,
                String = "abc",
                Enum = TestEnum.Value2
            },
            new TestItem()
            {
                Int = 1,
                NullInt = 1,
                Double = 2.5,
                NullDouble = 2.5,
                Date = new DateTime(2020, 2, 20),
                NullDate = new DateTime(2020, 2, 20),
                Bool = true,
                NullBool = false,
                String = "abc",
                Enum = TestEnum.Value1
            },
            new TestItem()
            {
                Int = 1,
                NullInt = null,
                Double = 2.5,
                NullDouble = null,
                Date = new DateTime(2020, 2, 20),
                NullDate = null,
                Bool = true,
                NullBool = null,
                String = "abc",
                Enum = TestEnum.Value2
            },
        };
    }
}
