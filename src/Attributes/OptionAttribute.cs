using System;

namespace coreArgs.Attributes
{
    ///<summary>
    /// Attribute to decorate an property as an **Option** for the **coreArgs** parser.
    ///</summary>
    public class OptionAttribute : Attribute
    {
        public OptionAttribute(char shortOption, string longOption, string helpText, bool required = false)
        {
            ShortOption = shortOption;
            LongOption = longOption;
            HelpText = helpText;
            Required = required;
        }

        public OptionAttribute(string longOption, string helpText, bool required = false)
        {
            LongOption = longOption;
            HelpText = helpText;
            Required = required;
        }

        public char ShortOption { get; set; }
        public string LongOption { get; set; }

        public string HelpText { get; set; }
        public bool Required { get; set; }
    }
}