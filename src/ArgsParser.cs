using System;
using coreArgs.Parser;

namespace coreArgs
{
    public class ArgsParser
    {
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
                result.Errors.Add(
                    new ParserError(ParserErrorType.UnknownError,
                                    ex.Message)
                );
            }
            return result;
        }
    }
}
