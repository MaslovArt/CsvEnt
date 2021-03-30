using System;
using System.Reflection;

namespace CsvEnt.Write
{
    /// <summary>
    /// Entity to csv write rule
    /// </summary>
    internal class WriteRule
    {
        internal WriteRule(int excelColInd, PropertyInfo prop, Func<object, string> map)
        {
            ColInd = excelColInd;
            Prop = prop;
            Map = map;
        }

        /// <summary>
        /// Entity property
        /// </summary>
        internal PropertyInfo Prop { get; set; }

        /// <summary>
        /// Csv row column index
        /// </summary>
        internal int ColInd { get; set; }

        /// <summary>
        /// Entity prop to csv value
        /// </summary>
        internal Func<object, string> Map { get; set; }
    }
}
