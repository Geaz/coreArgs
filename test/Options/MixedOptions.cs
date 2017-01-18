using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs.Tests.Options
{
    public class MixedOptions
    {        
        [Option("required", "The required option", true)]
        public string RequiredStringOption { get; set; }

        [Option('s', "shortoption", "The shortoption")]
        public string ShortStringOption { get; set; }

        [RemainingOptions]
        public dynamic RemainingOptions { get;set; }

        [BinOptions]
        public List<string> BinOption { get;set; }
    }
}