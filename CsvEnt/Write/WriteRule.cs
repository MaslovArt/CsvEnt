using System.Reflection;

namespace CsvEnt.Write
{
    /// <summary>
    /// Entity to csv write rule
    /// </summary>
    internal class WriteRule
    {
        internal WriteRule(int excelColInd, PropertyInfo prop)
        {
            ColInd = excelColInd;
            Prop = prop;
        }

        /// <summary>
        /// Entity property
        /// </summary>
        internal PropertyInfo Prop { get; set; }

        /// <summary>
        /// Csv row column index
        /// </summary>
        internal int ColInd { get; set; }
    }
}
