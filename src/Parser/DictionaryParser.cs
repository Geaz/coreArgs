using System.Collections.Generic;

namespace coreArgs.Parser
{    
    public class DictionaryParser
    {
        public Dictionary<string, string> ParseArgumentsIntoDic(string[] args)
        {
            var argumentsDictionary = new Dictionary<string, string>();

            if(args.Length > 0)
            {
                var possibleOption = string.Empty;
                foreach(var arg in args)
                {
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

                // If there is a possibleOption left after running through all args, it has to be a bool option
                if(!string.IsNullOrEmpty(possibleOption))
                {
                    argumentsDictionary.Add(possibleOption, "true");
                }
            }         
            return argumentsDictionary;
        }
    }
}