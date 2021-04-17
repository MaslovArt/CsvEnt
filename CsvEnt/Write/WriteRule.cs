using System;
using System.Reflection;

namespace CsvEnt.Write
{
    /// <summary>
    /// Entity to csv write rule
    /// </summary>
    internal class WriteRule
    {
        internal WriteRule(PropertyInfo prop, Func<object, string> map)
        {
            Prop = prop;
            Map = map;
        }

        /// <summary>
        /// Entity property
        /// </summary>
        internal PropertyInfo Prop { get; set; }

        /// <summary>
        /// Entity prop to csv value
        /// </summary>
        internal Func<object, string> Map { get; set; }
    }
}
