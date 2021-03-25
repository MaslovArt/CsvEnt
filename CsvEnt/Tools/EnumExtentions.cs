using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CsvEnt.Tools
{
    internal static class EnumExtentions
    {
        internal static string ToDescription<T>(this T @enum) =>
            @enum.GetType().GetField(@enum.ToString()).DescriptionAttrValue();

        internal static Enum ToEnumByDesc<Enum>(this string description)
        {
            var type = typeof(Enum);
            if (!type.IsEnum)
                throw new ArgumentException();

            foreach (var field in type.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (Enum)field.GetValue(null);
                }
            }

            throw new ArgumentException(description);
        }
    }
}
