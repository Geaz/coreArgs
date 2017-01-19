using System;

namespace coreArgs.Attributes
{
    ///<summary>
    /// Attribute to decorate an property as an **Option** for the **coreArgs** parser.
    ///</summary>
    public class OptionAttribute : Attribute
    {
        /// <summary>
        /// Constructor to mark a property with a **shortOption** and **longOption**.
        /// </summary>
        /// <param name="shortOption">Character which identifies the option in the command line arguments.</param>
        /// <param name="longOption">String which identifies the option in the command line arguments.</param>
        /// <param name="helpText">The help text to get with the <see cref="ArgsParser.GetHelpText{T}"/> method.</param>
        /// <param name="required">Define, if the option is required or not. Default is false.</param>
        public OptionAttribute(char shortOption, string longOption, string helpText, bool required = false)
        {
            ShortOption = shortOption;
            LongOption = longOption;
            HelpText = helpText;
            Required = required;
        }

        /// <summary>
        /// Constructor to mark a property with a **longOption**.
        /// </summary>
        /// <param name="longOption">String which identifies the option in the command line arguments.</param>
        /// <param name="helpText">The help text to get with the <see cref="ArgsParser.GetHelpText{T}"/> method.</param>
        /// <param name="required">Define, if the option is required or not. Default is false.</param>
        public OptionAttribute(string longOption, string helpText, bool required = false)
        {
            LongOption = longOption;
            HelpText = helpText;
            Required = required;
        }

        /// <summary>
        /// Gets the <c>char</c> which identifies the option in the command line arguments.
        /// </summary>
        public char? ShortOption { get; }

        /// <summary>
        /// Gets the <c>string</c> which identifies the option in the command line arguments.
        /// </summary>
        public string LongOption { get; }

        /// <summary>
        /// Gets the help text for this option.
        /// </summary>
        public string HelpText { get; }

        /// <summary>
        /// Returns true, if the option is required. False otherwise.
        /// </summary>
        public bool Required { get; }
    }
}