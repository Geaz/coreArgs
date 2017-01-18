using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs.Tests.Options
{
    public class RequiredOptions
    {
        [Option("longstring", "The longoption", true)]
        public string LongStringOption { get; set; }
    }
}