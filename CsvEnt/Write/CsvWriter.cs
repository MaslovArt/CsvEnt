using CsvEnt.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace CsvEnt.Write
{
    public class CsvWriter<T>
    {
        private char            _separator;
        private List<WriteRule> _rules;
        private string[]        _columnsTitles;

        public CsvWriter()
        {
            _separator = ';';
            _rules = new List<WriteRule>();
        }

        public CsvWriter<T> WithSeparator(char separator)
        {
            _separator = separator;

            return this;
        }

        public CsvWriter<T> AddRule(Expression<Func<T, object>> propName, int colIndex)
        {
            var prop = TypeExtentions.GetProperty(propName);
            _rules.Add(new WriteRule(colIndex, prop));

            return this;
        }

        public CsvWriter<T> AddColumnsTitles(params string[] titles)
        {
            _columnsTitles = titles;

            return this;
        }

        public void Write(IEnumerable<T> entities, string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                using (StreamWriter writetext = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (var entity in entities)
                    {
                        var sb = new StringBuilder();
                        foreach (var rule in _rules)
                        {
                            var value = rule.Prop.GetValue(entity);
                            sb.Append(value).Append(_separator);
                        }
                        sb.Remove(sb.Length - 1, 1);
                    }
                }
            }
        }
    }
}
