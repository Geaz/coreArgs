using coreArgs.Parser;

namespace coreArgs
{
    public class ArgsParser
    {
        public static T Parse<T>(string[] args)
        {
            var dictionaryParser = new DictionaryParser();
            var objectParser = new ObjectParser<T>();

            var argumentsDictionary = dictionaryParser.ParseArgumentsIntoDic(args);
            return objectParser.MapArgumentsIntoObject(argumentsDictionary);
        }
    }
}
