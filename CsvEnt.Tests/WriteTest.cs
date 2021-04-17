using CsvEnt.Tests.ExpectedData;
using CsvEnt.Tests.Models;
using CsvEnt.Write;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

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
                    .AddRule(e => e.Int)
                    .AddRule(e => e.NullInt)
                    .AddRule(e => e.Double)
                    .AddRule(e => e.NullDouble)
                    .AddRule(e => e.Bool)
                    .AddRule(e => e.NullBool)
                    .AddRule(e => e.Date, v => v.ToString("dd.MM.yyyy"))
                    .AddRule(e => e.NullDate, v => v?.ToString("dd.MM.yyyy"))
                    .AddRule(e => e.String)
                    .AddRule(e => e.Enum)
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
                    .AddRule(e => e.Int)
                    .AddRule(e => e.NullInt)
                    .AddRule(e => e.Double)
                    .AddRule(e => e.NullDouble)
                    .AddRule(e => e.Bool)
                    .AddRule(e => e.NullBool)
                    .AddRule(e => e.Date, v => v.ToString("dd.MM.yyyy"))
                    .AddRule(e => e.NullDate, v => v?.ToString("dd.MM.yyyy"))
                    .AddRule(e => e.String)
                    .AddRule(e => e.Enum)
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
