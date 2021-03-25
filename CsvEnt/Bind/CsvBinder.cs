using CsvEnt.Exceptions;
using CsvEnt.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace CsvEnt.Bind
{
    /// <summary>
    /// Csv to entities binder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvBinder<T> where T : new()
    {
        private bool            _continueOrError;
        private char            _separator;
        private int             _skip;
        private int             _take;
        private List<BindRule>  _rules;

        /// <summary>
        /// Binding errors
        /// </summary>
        public List<CsvBindException> Errors { get; private set; }

        public CsvBinder()
        {
            Errors = new List<CsvBindException>();
            _rules = new List<BindRule>();
            _separator = ';';
        }

        /// <summary>
        /// Set behavior on error
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public CsvBinder<T> ContinueOrError(bool flag)
        {
            _continueOrError = flag;

            return this;
        }

        /// <summary>
        /// Number of lines to bind
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public CsvBinder<T> Take(int count)
        {
            CheckValue(count, 0);
            _take = count;

            return this;
        }

        /// <summary>
        /// Number of lines to skip
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public CsvBinder<T> Skip(int count)
        {
            CheckValue(count, 0);
            _skip = count;

            return this;
        }

        /// <summary>
        /// Csv data delimiter
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public CsvBinder<T> WithSeparator(char separator)
        {
            _separator = separator;

            return this;
        }

        /// <summary>
        /// Add csv value to entity property rule
        /// </summary>
        /// <param name="colIndex">Csv row column index</param>
        /// <param name="propName">Entity prop name</param>
        /// <param name="map">Map function</param>
        /// <returns></returns>
        public CsvBinder<T> AddRule(int colIndex, Expression<Func<T, object>> propName, Func<string, object> map)
        {
            var prop = TypeExtentions.GetProperty(propName);
            _rules.Add(new BindRule(colIndex, prop, map));

            return this;
        }

        /// <summary>
        /// Bind csv to entities
        /// </summary>
        /// <param name="csvFilePath">Csv file path</param>
        /// <returns></returns>
        public T[] Bind(string csvFilePath)
        {
            using (StreamReader sr = new StreamReader(csvFilePath))
            {
                var currentLineInd = 0;
                while (currentLineInd < _take + _skip && sr.Peek() >= 0)
                {
                    var currentLineStr = sr.ReadLine();

                    if (currentLineInd < _skip) continue;

                    var currentLine = currentLineStr.Split(_separator);

                    var newEntity = new T();
                    foreach (var rule in _rules)
                    {
                        SetEntityValue(newEntity, rule, currentLine, currentLineInd);
                    }

                    currentLineInd++;
                }
            }

            return null;
        }

        private void SetEntityValue(T entity, BindRule rule, string[] columnValues, int rowInd)
        {
            try
            {
                var columnValue = columnValues[rule.ColInd];
                var mappedValue = rule.Map(columnValue);

                rule.Prop.SetValue(entity, mappedValue);
            }
            catch (Exception ex)
            {
                var bindEx = new CsvBindException(columnValues, rowInd, rule.ColInd, ex);
                if (_continueOrError)
                {
                    Errors.Add(bindEx);
                    return;
                }

                throw bindEx;
            }
        }

        private void CheckValue(int value, int min)
        {
            if (value < min)
                throw new ArgumentException($"Value less then ${min}. (${value})");
        }
    }
}
