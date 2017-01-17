using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using coreArgs.Attributes;

namespace coreArgs.Parser
{
    internal class ObjectParser<T>
    {
        public T MapArgumentsIntoObject(Dictionary<string, string> arguments)
        {
            var options = (T)Activator.CreateInstance<T>();
            var optionsProperties = options.GetType().GetProperties();
            var binList = GetBinList(options, optionsProperties);
            var remainingOptions = GetRemainingOptionsDynamic(options, optionsProperties);

            foreach(var argument in arguments)
            {
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
                            property.SetValue(options, argument.Value);
                            break;
                        }
                    }
                }                
            }
            return options;
        }

        private dynamic GetRemainingOptionsDynamic(T options, PropertyInfo[] properties)
        {
            dynamic remainingOptions = null;
            var remainingOptionsProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<RemainingOptionsAttribute>() != null);
            if(remainingOptionsProperty != null)
            {
                remainingOptions = remainingOptionsProperty.GetValue(options) as dynamic;
                if(remainingOptions == null) throw new Exception("RemainingOptions was found, but was not defined as a 'dynamic' Property!");
            }   
            return remainingOptions;
        }

        private List<string> GetBinList(T options, PropertyInfo[] properties)
        {
            List<string> binList = null;
            var binOption = properties.FirstOrDefault(p => p.GetCustomAttribute<BinOptionsAttribute>() != null);
            if(binOption != null)
            {
                binList = binOption.GetValue(options) as List<string>;
                if(binList == null) throw new Exception("BinOptions was found, but was not defined as a 'List<string>' Property!");
            }   
            return binList;
        }
    }
}