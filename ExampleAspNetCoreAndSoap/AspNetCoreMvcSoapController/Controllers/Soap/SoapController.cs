using System.Threading;
using System.Threading.Tasks;
using AspNetCoreMvcSoapController.Soap.V11;
using AspNetCoreMvcSoapController.Soap.V12;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcSoapController.Controllers.Soap
{
    public abstract class SoapController : Controller
    {
        [SoapV11Action, Route("")]
        public async Task<SoapV11Envelope> SoapEnvelope11([FromBody] SoapV11Envelope envelope, CancellationToken ct)
        {
            return null;
        }

        [SoapV12Action, Route("")]
        public async Task<SoapV12Envelope> SoapEnvelope12([FromBody] SoapV12Envelope envelope, CancellationToken ct)
        {
            return null;
        }
    }
}