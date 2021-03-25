using System.ComponentModel;

namespace CsvEnt.Tests.Models
{
    public enum TestEnum
    {
        [Description("val1")]
        Value1,
        [Description("val2")]
        Value2,
        [Description("val3")]
        Value3
    }
}
