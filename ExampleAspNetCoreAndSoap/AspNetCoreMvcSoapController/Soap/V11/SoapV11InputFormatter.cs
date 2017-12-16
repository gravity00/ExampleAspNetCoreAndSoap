using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreMvcSoapController.Soap.V11
{
    public class SoapV11InputFormatter : TextInputFormatter
    {
        private static readonly MediaTypeHeaderValue TextXml
            = MediaTypeHeaderValue.Parse("text/xml").CopyAsReadOnly();

        private static readonly XmlSerializer EnvelopeSerializer = new XmlSerializer(typeof(SoapV11Envelope));

        public SoapV11InputFormatter()
        {
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);

            SupportedMediaTypes.Add(TextXml);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            var request = context.HttpContext.Request;

            if (!request.Body.CanSeek)
                request.EnableRewind();

            //  XML serializer does synchronous reads. This loads everything
            //  into the buffer without blocking
            await request.Body.DrainAsync(CancellationToken.None);
            request.Body.Seek(0L, SeekOrigin.Begin);

            SoapV11Envelope soapEnvelope;
            using (var stream = new NonDisposableStream(request.Body))
            using (var reader = new XmlTextReader(stream))
            {
                soapEnvelope = (SoapV11Envelope) EnvelopeSerializer.Deserialize(reader);
            }

            if (context.ModelType == typeof(SoapV11Envelope))
                return InputFormatterResult.Success(soapEnvelope);

            using (var stream = new StringReader(soapEnvelope.Body.ToString()))
            using (var reader = new XmlTextReader(stream))
            {
                return InputFormatterResult.Success(
                    new XmlSerializer(context.ModelType).Deserialize(reader));
            }
        }

        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            //  SOAP Action header is required but it may have an empty value
            //  https://www.w3.org/TR/2000/NOTE-SOAP-20000508/#_Toc478383528
            //  
            //  In this example, the HTTP Extension Framework won't be supported
            return base.CanRead(context) &&
                   context.HttpContext.Request.Headers.Any(header =>
                       "SOAPAction".Equals(header.Key, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
