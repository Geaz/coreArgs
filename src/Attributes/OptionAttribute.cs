using System;

namespace coreArgs.Attributes
{
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