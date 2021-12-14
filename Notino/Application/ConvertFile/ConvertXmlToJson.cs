using MediatR;
using Notino.Contracts;
using Notino.Converters;
using System.Threading;
using System.Threading.Tasks;

namespace Notino.Application.ConvertFile
{
    public record ConvertXmlToJsonQuery(string Content) : IRequest<Result<ConvertXmlToJsonResponse>>;
    public class ConvertXmlToJsonResponse
    {
        public string ConvertedContent { get; set; }
    }
    public class ConvertXmlToJsonHandler : IRequestHandler<ConvertXmlToJsonQuery, Result<ConvertXmlToJsonResponse>>
    {
        private readonly ConverterProviderFactory _converterProviderFactory;

        public ConvertXmlToJsonHandler(ConverterProviderFactory converterProviderFactory)
        {

            _converterProviderFactory = converterProviderFactory;
        }

        public async Task<Result<ConvertXmlToJsonResponse>> Handle(ConvertXmlToJsonQuery request, CancellationToken cancellationToken)
        {
            var converterProvider = _converterProviderFactory.GetConverterProvider(ConversionType.XmlToJson);
            var result = converterProvider.Convert(request.Content);

            if (result.Failed)
                return Result<ConvertXmlToJsonResponse>.Failure(result.Errors);

            return Result<ConvertXmlToJsonResponse>.Success(new ConvertXmlToJsonResponse { ConvertedContent = result.Data });
        }
    }
}
