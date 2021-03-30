using CsvEnt.Tests.ExpectedData;
using CsvEnt.Tests.Models;
using CsvEnt.Write;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvEnt.Tests
{
    [TestClass]
    public class WriteTest
    {
        [TestMethod]
        public void WriteTitledTest()
        {
            string testFilePath = Path.GetTempFileName();

            try
            {
                new CsvWriter<TestItem>()
                    .AddRule(e => e.Int, 0)
                    .AddRule(e => e.NullInt, 1)
                    .AddRule(e => e.Double, 2)
                    .AddRule(e => e.NullDouble, 3)
                    .AddRule(e => e.Bool, 4)
                    .AddRule(e => e.NullBool, 5)
                    .AddRule(e => e.Date, 6, v => ((DateTime)v).ToString("dd.MM.yyyy"))
                    .AddRule(e => e.NullDate, 7, v => ((DateTime?)v)?.ToString("dd.MM.yyyy"))
                    .AddRule(e => e.String, 8)
                    .AddRule(e => e.Enum, 9)
                    .AddColumnsTitles(new string[]
                    {
                    "Int", "NullInt",
                    "Double", "NullDouble",
                    "Bool", "NullBool",
                    "Date", "NullDate",
                    "String",
                    "Enum"
                    })
                    .WithDelimiter(';')
                    .Write(Expected.Data, testFilePath);

                AssertFilesEquals(testFilePath, Expected.TestItemsDataFile, 0);
            }
            finally
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void WriteNoTitledTest()
        {
            string testFilePath = Path.GetTempFileName();

            try
            {
                new CsvWriter<TestItem>()
                    .AddRule(e => e.Int, 0)
                    .AddRule(e => e.NullInt, 1)
                    .AddRule(e => e.Double, 2)
                    .AddRule(e => e.NullDouble, 3)
                    .AddRule(e => e.Bool, 4)
                    .AddRule(e => e.NullBool, 5)
                    .AddRule(e => e.Date, 6, v => ((DateTime)v).ToString("dd.MM.yyyy"))
                    .AddRule(e => e.NullDate, 7, v => ((DateTime?)v)?.ToString("dd.MM.yyyy"))
                    .AddRule(e => e.String, 8)
                    .AddRule(e => e.Enum, 9)
                    .WithDelimiter(';')
                    .Write(Expected.Data, testFilePath);

                AssertFilesEquals(testFilePath, Expected.TestItemsDataFile, 1);
            }
            finally
            {
                File.Delete(testFilePath);
            }
        }

        private void AssertFilesEquals(string testFilePath, string expectedFilePath, int expectedSkipLines)
        {
            var testLines = File.ReadAllLines(testFilePath);
            var expectedLines = File.ReadAllLines(expectedFilePath);

            expectedLines = expectedLines.Skip(expectedSkipLines).ToArray();

            CollectionAssert.AreEqual(testLines, expectedLines);
        }
    }
}
