using System;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

namespace coreArgs.Parser
{
    public class TypeParser
    {
        public TypeParserResult Parse(Type type, string value)
        {
            var result = new TypeParserResult { Value = value };
            try
            {
                if (type.Name == typeof(List<>).Name)
                {
                    var parseMethod = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                        .Single(m => m.Name == "Parse" && m.GetGenericArguments().Length > 0);
                    parseMethod = parseMethod.MakeGenericMethod(type.GetGenericArguments()[0]);

                    result.Value = parseMethod.Invoke(this, new object[] { value });
                }
                else if (type != typeof(string))
                {
                    var parseCultureVariant = type.GetMethod("Parse", new[] { typeof(string), typeof(CultureInfo) });
                    var parse = type.GetMethod("Parse", new[] { typeof(string) });
                    if (parseCultureVariant == null && parse == null)
                    {
                        result.Error = new ParserError(ParserErrorType.ValueParseError, $"No parsing method found for type '{type.Name}'!");
                    }
                    else
                    {
                        result.Value = parseCultureVariant != null
                                    ? parseCultureVariant.Invoke(null, new object[] { value, CultureInfo.InvariantCulture })
                                    : parse.Invoke(null, new object[] { value });
                    }
                }
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                result.Error = new ParserError(
                    ParserErrorType.ValueParseError,
                    $"Couldn't parse '{value}' to type '{type.Name}'. Inner Exception: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {
                result.Error = new ParserError(
                    ParserErrorType.ValueParseError,
                    $"Couldn't parse '{value}' to type '{type.Name}'. Exception: {ex.Message}");
            }
            return result;
        }

        private List<TK> Parse<TK>(string values)
        {
            var valueList = new List<TK>();

            var singleValues = values.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var singleValue in singleValues)
            {
                valueList.Add((TK)Parse(typeof(TK), singleValue.Trim()).Value);
            }

            return valueList;
        }
    }
}
