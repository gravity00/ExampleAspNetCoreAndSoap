using System.Xml.Serialization;

namespace AspNetCoreMvcSoapController.Soap.V11
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapV11Envelope
    {
        [XmlElement("Header")]
        public SoapV11EnvelopeHeader Header { get; set; } = new SoapV11EnvelopeHeader();

        [XmlElement("Body")]
        public SoapV11EnvelopeBody Body { get; set; } = new SoapV11EnvelopeBody();
    }
}
