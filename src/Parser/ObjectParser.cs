using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs.Parser
{
    internal class ObjectParser<T>
    {
        private ParserResult<T> _parserResult;

        private readonly TypeParser _typeParser;

        public ObjectParser()
        {
            _typeParser = new TypeParser();
        }

        public ParserResult<T> MapArgumentsIntoObject(Dictionary<string, string> arguments)
        {
            // Set the private field to a new result. The whole class will use this to 
            // save generated errors during the parsing process.
            _parserResult = new ParserResult<T>();

            var options = (T)Activator.CreateInstance<T>();
            var optionsProperties = options.GetType().GetProperties();
            var binList = GetAndSetBinList(options, optionsProperties);
            var remainingOptions = GetAndSetRemainingOptionsDynamic(options, optionsProperties);

            foreach(var argument in arguments)
            {
                var argumentHandled = false;
                if(string.IsNullOrEmpty(argument.Value) && binList != null)
                {
                    binList.Add(argument.Key);
                }
                else
                {
                    foreach(var property in optionsProperties)
                    {
                        var optionAttribute = property.GetCustomAttribute<OptionAttribute>();            
                        if(optionAttribute != null && (optionAttribute.ShortOption.ToString() == argument.Key || optionAttribute.LongOption == argument.Key))
                        {                     
                            var propertyType = property.PropertyType;
                            var typeParserResult = _typeParser.Parse(propertyType, argument.Value);

                            if (typeParserResult.Error != null) _parserResult.Errors.Add(typeParserResult.Error);
                            else property.SetValue(options, typeParserResult.Value);

                            argumentHandled = true;
                            break;
                        }
                    }
                    if(!argumentHandled && remainingOptions != null)
                    {
                        ((IDictionary<string, Object>)remainingOptions).Add(argument.Key, argument.Value);
                    }
                }                
            }

            CheckRequiredProperties(options, optionsProperties);

            _parserResult.Arguments = options;
            return _parserResult;
        }

        private void CheckRequiredProperties(T options, PropertyInfo[] properties)
        {
            foreach(var property in properties)
            {
                var optionAttr = property.GetCustomAttribute<OptionAttribute>();
                if(optionAttr != null && optionAttr.Required)
                {
                    var optionValue = property.GetValue(options);
                    if(optionValue == null) 
                    {
                        _parserResult.Errors.Add(
                            new ParserError(ParserErrorType.RequiredPropertyNotSet,
                                            $"Property '{property.Name}' is required, but not set!")
                        );
                    }
                }
            }
        }

        private List<string> GetAndSetBinList(T options, PropertyInfo[] properties)
        {
            List<string> binList = null;
            var binOptionProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<BinOptionsAttribute>() != null);
            if(binOptionProperty != null)
            {
                if(binOptionProperty.PropertyType.Name != typeof(List<>).Name) 
                {
                    _parserResult.Errors.Add(
                        new ParserError(ParserErrorType.WrongPropertyType, 
                                        "BinOptions was found, but was not defined as a 'List<string>' Property!")
                    );                    
                }
                else
                {
                    binList = new List<string>();
                    binOptionProperty.SetValue(options, binList);   
                } 
            }   
            return binList;
        }

        private dynamic GetAndSetRemainingOptionsDynamic(T options, PropertyInfo[] properties)
        {
            dynamic remainingOptions = null;
            var remainingOptionsProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<RemainingOptionsAttribute>() != null);
            if(remainingOptionsProperty != null)
            {
                remainingOptions = new UndefinedArgs();
                remainingOptionsProperty.SetValue(options, remainingOptions);
            }   
            return remainingOptions;
        }
    }
}