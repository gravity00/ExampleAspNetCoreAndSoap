using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreMvcSoapController.Soap.V12
{
    public class SoapV12InputFormatter : TextInputFormatter
    {
        public static readonly MediaTypeHeaderValue ApplicationSoapXml
            = MediaTypeHeaderValue.Parse("application/soap+xml").CopyAsReadOnly();

        public SoapV12InputFormatter()
        {
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);

            SupportedMediaTypes.Add(ApplicationSoapXml);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanRead(InputFormatterContext context)
        {
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
