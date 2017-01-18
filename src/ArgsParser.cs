using System;
using coreArgs.Parser;

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
    }
}
