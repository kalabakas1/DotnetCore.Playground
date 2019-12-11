namespace Playground.Domain.Constants
{
    public static class ExceptionMessage
    {
        public const string NotValidUrl = "The defined URL {0} is not valid";
        public const string ParameterMustBeDefined = "The following parameter must be defined: {0}";
        public const string ParamaterNotValidType = "The following parameter is not of valid type: {0}";
        public const string NoValueFound = "No valid value found";
        public const string NotCorrectResult = "Did not return expected result - returned: {0}";
        public const string ObjectNull = "Object not initialized: {0}";
        public const string MustContainValue = "Must contain one of the following values: {0}";
    }
}