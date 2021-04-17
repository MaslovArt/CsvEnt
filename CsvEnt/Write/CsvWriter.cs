using CsvEnt.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace CsvEnt.Write
{
    /// <summary>
    /// Entities to csv writer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvWriter<T>
    {
        private char            _delimiter;
        private List<WriteRule> _rules;
        private string[]        _columnsTitles;

        public CsvWriter()
        {
            _delimiter = ';';
            _rules = new List<WriteRule>();
        }

        /// <summary>
        /// Add line column delimiter
        /// </summary>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public CsvWriter<T> WithDelimiter(char delimiter)
        {
            _delimiter = delimiter;

            return this;
        }

        /// <summary>
        /// Add property to line column value mapper
        /// </summary>
        /// <param name="propName">Property name</param>
        /// <param name="colIndex">Line column index</param>
        /// <param name="map">Map func</param>
        /// <returns></returns>
        public CsvWriter<T> AddRule<P>(Expression<Func<T, P>> propName, Func<P, string> map = null)
        {
            var prop = TypeExtentions.GetProperty(propName);
            if (map == null)
                _rules.Add(new WriteRule(prop, null));
            else
                _rules.Add(new WriteRule(prop, (v) => map((P)v)));

            return this;
        }

        /// <summary>
        /// Add head line with titles
        /// </summary>
        /// <param name="titles"></param>
        /// <returns></returns>
        public CsvWriter<T> AddColumnsTitles(params string[] titles)
        {
            _columnsTitles = titles;

            return this;
        }

        /// <summary>
        /// Write entitites to file
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="filePath">Result file path</param>
        public void Write(IEnumerable<T> entities, string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    AddTitlesIfNeed(writer);

                    foreach (var entity in entities)
                    {
                        var textLine = new StringBuilder();
                        foreach (var rule in _rules)
                        {
                            var value = rule.Prop.GetValue(entity);
                            if (rule.Map != null)
                            {
                                value = rule.Map(value);
                            }
                            textLine.Append(value).Append(_delimiter);
                        }
                        if (textLine.Length > 0) 
                            textLine.Remove(textLine.Length - 1, 1);

                        writer.WriteLine(textLine.ToString());
                    }
                }
            }
        }

        private void AddTitlesIfNeed(StreamWriter writer)
        {
            if (_columnsTitles != null && _columnsTitles.Length > 0)
            {
                writer.WriteLine(string.Join(_delimiter.ToString(), _columnsTitles));
            }
        }
    }
}
