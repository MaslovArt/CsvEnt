using CsvEnt.Tools;
using System;

namespace CsvEnt.Bind
{
    public static class BindMappers
    {
        /// <summary>
        /// Parse column value to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Int(string value) => 
            int.Parse(value);

        /// <summary>
        /// Parse column value to nullable int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object NullInt(string value) =>
            string.IsNullOrEmpty(value) ? null : Int(value);

        /// <summary>
        /// Parse column value to double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Double(string value) =>
            double.Parse(value);

        /// <summary>
        /// Parse column value to nullable double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object NullDouble(string value) =>
            string.IsNullOrEmpty(value) ? null : Double(value);

        /// <summary>
        /// Parse column value to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string String(string value) => value;

        /// <summary>
        /// Parse column value to date
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static object Date(string value, string format) =>
            DateTime.ParseExact(value, format, null);

        /// <summary>
        /// Parse column value to nullable date
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static object NullDate(string value, string format) =>
            string.IsNullOrEmpty(value) ? null : Date(value, format);

        /// <summary>
        /// Parse column value to bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trueValue"></param>
        /// <returns></returns>
        public static object Bool(string value, string trueValue) =>
            value == trueValue;

        /// <summary>
        /// Parse column value to nullable bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trueValue"></param>
        /// <returns></returns>
        public static object NullBool(string value, string trueValue) =>
            string.IsNullOrEmpty(value) ? null : Bool(value, trueValue);

        /// <summary>
        /// Parse column value to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ValueEnum<T>(string value) =>
            (T)Enum.Parse(typeof(T), value);

        /// <summary>
        /// Parse column value to enum by enum description attr
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object DescEnum<T>(string value) =>
            value.ToEnumByDesc<T>();
    }
}
