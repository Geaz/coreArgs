using System.Collections.Generic;

namespace coreArgs.Parser
{    
    ///<summary>
    /// The <c>DictionaryParser</c> parser is the first step in parsing the command line arguments.
    /// It is responsible for parsing each argument into a <c>Dictionary&lt;string, string&gt;</c>.
    /// Some examples:
    /// 
    /// - An argument like **--option text** is parsed as **Key:** *option*, **Value:** *text*
    /// - An argument like **--booloption** is parsed as **Key:** *booloption*, **Value:** *true*
    /// - An argument like **binOption** is parsed as **Key:** *binOption*, **Value:** *string.Empty*
    ///
    /// This is done to make it easy for the <c>ObjectParser</c> to parse the arguments into an object.
    ///</summary>
    public class DictionaryParser
    {
        public Dictionary<string, string> ParseArgumentsIntoDic(string[] args)
        {
            var argumentsDictionary = new Dictionary<string, string>();
            
            //If we don't have any arguments, the Parser has nothing to do.
            if(args.Length == 0) return argumentsDictionary;

            // Now we iterate through all arguments and make decisions about each element,
            // based on the previous argument (possibleOption).                        
            var possibleOption = string.Empty;
            foreach(var arg in args)
            {
                // If the current argument starts with a "-" and there is no "possibleOption" saved,
                // it could be a "boolOption" or "option" (string, int, double etc. we don't make and decisions
                // about the type in this parser).
                // Otherwise, if there is already a "possibleOption" saved, the current argument gets the
                // next "possibleOption" and the old "possibleOption" has to be a "boolOption".
                if(arg.StartsWith("-"))
                {
                    //PossibleOption
                    if(string.IsNullOrEmpty(possibleOption))
                    {
                        possibleOption = arg.TrimStart('-');
                    }
                    //BoolOption
                    else
                    {
                        argumentsDictionary.Add(possibleOption, "true");
                        possibleOption = arg.TrimStart('-');
                    }
                }
                // If the current argument doesn't start with a "-" and there is a "possibleOption" saved,
                // it has to be a "option". Otherwise, if there is no "possibleOption" saved, the current argument
                // has to be a "binOption".
                else
                {
                    //Option
                    if(!string.IsNullOrEmpty(possibleOption))
                    {
                        argumentsDictionary.Add(possibleOption, arg);
                        possibleOption = string.Empty;
                    }
                    //BinOption
                    else
                    {
                        argumentsDictionary.Add(arg, string.Empty);
                    }
                }
            }

            // If there is a possibleOption left after running through all args, it has to be a bool option.
            if(!string.IsNullOrEmpty(possibleOption))
            {
                argumentsDictionary.Add(possibleOption, "true");
            }       
            return argumentsDictionary;
        }
    }
}