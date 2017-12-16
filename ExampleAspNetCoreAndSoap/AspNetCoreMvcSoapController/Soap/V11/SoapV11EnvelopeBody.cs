using System.Xml.Linq;
using System.Xml.Serialization;

namespace AspNetCoreMvcSoapController.Soap.V11
{
    public class SoapV11EnvelopeBody
    {
        [XmlAnyElement]
        public XElement Value { get; set; }
    }
}