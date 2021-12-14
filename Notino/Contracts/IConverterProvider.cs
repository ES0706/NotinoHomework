namespace Notino.Contracts
{
    public interface IConverterProvider
    {
        public Result<string> Convert(string input);
        public ConversionType ConversionType { get; }
    }

    public enum ConversionType
    {
        XmlToJson
    }
}
