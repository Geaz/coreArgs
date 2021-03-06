﻿using System;
using System.Reflection;

using coreArgs.Model;
using coreArgs.Parser;
using coreArgs.Attributes;
using System.Text;

namespace coreArgs
{
    ///<summary>
    /// This is the core parser of **coreArgs**.
    /// Use this type to parse command line arguments into a defined type.
    ///</summary>
    public class ArgsParser
    {
        /// <summary>
        /// Use this method to parse command line arguments.
        /// </summary>
        /// <param name="args">The command line argument to parse</param>
        /// <returns>A <see cref="ParserResult{T}"/> including the parsed object and all parsing errors.</returns>
        public static ParserResult<T> Parse<T>(string[] args)
        {
            var result = new ParserResult<T>();
            try
            {
                var dictionaryParser = new DictionaryParser();
                var objectParser = new ObjectParser<T>();

                var argumentsDictionary = dictionaryParser.ParseArgumentsIntoDic(args);
                result = objectParser.MapArgumentsIntoObject(argumentsDictionary);
            }
            catch(Exception ex)
            {
                result.Errors.Add(new ParserError(ParserErrorType.UnknownError, ex.Message));
            }
            return result;
        }

        /// <summary>
        /// Returns the help text for the given option type.
        /// </summary>
        /// <typeparam name="T">Type which defines some option properties.</typeparam>
        /// <returns><c>string</c> containing the help text.</returns>
        public static string GetHelpText<T>()
        {
            var helpText = new StringBuilder();

            var properties = typeof(T).GetProperties();
            foreach(var property in properties)
            {
                var optionAttribute = property.GetCustomAttribute<OptionAttribute>();
                if (optionAttribute != null)
                {
                    // If someone knows a better way to build up this text,
                    // pull requests are welcome :)
                    var helpLine = optionAttribute.ShortOption != null
                        ? $"-{optionAttribute.ShortOption} | "
                        : "     ";

                    helpLine += $"--{optionAttribute.LongOption}";
                    helpLine += $"\t\t{optionAttribute.HelpText}";

                    helpLine += optionAttribute.Required
                        ? " (required)"
                        : string.Empty;

                    helpText.AppendLine(helpLine);
                }
            }

            return helpText.ToString();
        }
    }
}
