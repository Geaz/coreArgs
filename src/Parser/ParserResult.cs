using System.Collections.Generic;

namespace coreArgs.Parser
{
    ///<summary>
    /// The type to hold the results of the **coreArgs** <c>ArgsParser</c>.
    ///</summary>
    public class ParserResult<T>
    {
        public T Arguments { get; set; }
        public List<ParserError> Errors { get; set; } = new List<ParserError>();
    }
}