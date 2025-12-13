namespace BackEnd.Common.Core.Exceptions
{
    public enum ExceptionTypeCodes
    {
        DomainException = 100,
        ApplicationException = 200,
        ReadException = 300,
        PresentationException = 400
    }

    public enum BoundedContextCodes
    {
        Global = 1000,
        Core = 2000,
        Identity = 3000,
        Media = 4000,
        Io = 5000,
        Other = 9000
    }
}