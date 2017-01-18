using System.Collections.Generic;

namespace coreArgs.Parser
{
    public class ParserResult<T>
    {
        public T Arguments { get; set; }
        public List<ParserError> Errors { get; set; } = new List<ParserError>();
    }
}