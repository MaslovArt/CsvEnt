using System;

namespace CsvEnt.Exceptions
{
    public class CsvBindException : Exception
    {
        public CsvBindException(string[] rowValues, int row, int col, Exception ex)
            :base(ex.Message, ex)
        {
            RowValues = rowValues;
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
        public string[] RowValues { get; set; }
    }
}
