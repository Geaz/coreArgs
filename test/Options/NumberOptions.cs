using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs.Tests.Options
{
    public class NumberOptions
    {
        [Option('d', "decimal", "Decimal option")]
        public decimal DecimalOption { get; set; }

        [Option('i', "integer", "Integer option")]
        public int IntOption { get; set; }

        [Option("double", "Double option")]
        public double DoubleOption { get; set; }

        [Option("intlist", "The listoption")]
        public List<int> ListIntOptions { get; set; }

        [Option("decimallist", "The listoption")]
        public List<decimal> ListDecimalOptions { get; set; }

        [Option("doublelist", "The listoption")]
        public List<double> ListDoubleOptions { get; set; }
    }
}