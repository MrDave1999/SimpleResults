namespace SimpleResults.Example.Web.Tests;

public class Routes
{
    public class Person
    {
        public const string WebApi     = "/Person-WebApi";
        public const string MinimalApi = "/Person-MinimalApi";
    }

    public class User
    {
        public const string WebApi     = "/User-WebApi";
        public const string MinimalApi = "/User-MinimalApi";
    }

    public class Message
    {
        public const string WebApi     = "/Message-WebApi";
        public const string MinimalApi = "/Message-MinimalApi";
    }

    public class File
    {
        public const string ByteArrayController = "/FileResult-WebApi/byte-array";
        public const string StreamController    = "/FileResult-WebApi/stream";
        public const string ByteArrayMinimalApi = "/FileResult-MinimalApi/byte-array";
        public const string StreamMinimalApi    = "/FileResult-MinimalApi/stream";
    }

    public class Order
    {
        public const string ManualValidation    = "/Order-ManualValidation";
        public const string AutomaticValidation = "/Order-AutomaticValidation";
    }
}
