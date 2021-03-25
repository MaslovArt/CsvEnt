using System;
using System.Reflection;

namespace CsvEnt.Bind
{
    internal class BindRule
    {
        internal BindRule(int excelColInd, PropertyInfo prop, Func<string, object> mapper)
        {
            ColInd = excelColInd;
            Prop = prop;
            Map = mapper;
        }

        internal PropertyInfo Prop { get; set; }

        internal int ColInd { get; set; }

        internal Func<string, object> Map { get; set; }
    }
}
