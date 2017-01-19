namespace coreArgs.Model
{
    ///<summary>
    /// Defines the error type for **coreArgs**.
    /// Every error in the <c>ParserResult&lt;T&gt;</c> has a message and an <c>ErrorType</c>.
    /// During the parsing it is possible, that multiple errors occure. For example,
    /// if more than one required option is missing.
    ///</summary>
    public class ParserError
    {
        public ParserError(ParserErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }

        public string Message { get; set; }
        public ParserErrorType ErrorType { get; set; }
    }

    ///<summary>
    /// The possible error types for the <c>ParserError</c>.
    ///</summary>
    public enum ParserErrorType 
    {
        ValueParseError,
        WrongPropertyType,
        RequiredPropertyNotSet,
        UnknownError
    }
}