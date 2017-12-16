using System.Xml.Linq;
using System.Xml.Serialization;

namespace AspNetCoreMvcSoapController.Soap.V11
{
    public class SoapV11EnvelopeHeader
    {
        [XmlAnyElement]
        public XElement[] Headers { get; set; } = EmptyHeaders;

        private static readonly XElement[] EmptyHeaders = new XElement[0];
    }
}