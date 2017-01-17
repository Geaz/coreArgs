using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs
{
    public class TestOptions
    {
        [Option("longstring", "The longoption")]
        public string LongStringOption { get; set; }

        [BinOptions]
        public List<string> BinOption { get;set; }  = new List<string>();
    }
}