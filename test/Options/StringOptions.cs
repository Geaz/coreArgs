using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs.Tests.Options
{
    public class StringOptions
    {
        [Option("longstring", "The longoption")]
        public string LongStringOption { get; set; }

        [Option('s', "shortoption", "The shortoption")]
        public string ShortStringOption { get; set; }

        [Option('l', "listoption", "The listoption")]
        public List<string> ListStringOptions { get; set; }
    }
}