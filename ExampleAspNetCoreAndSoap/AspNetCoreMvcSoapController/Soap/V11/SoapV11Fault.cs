using System.Xml.Linq;
using System.Xml.Serialization;

namespace AspNetCoreMvcSoapController.Soap.V11
{
    [XmlRoot("Fault", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapV11Fault
    {
        [XmlElement("faultcode", Namespace = "")]
        public string Code { get; set; }

        [XmlElement("faultstring", Namespace = "")]
        public string String { get; set; }

        [XmlElement("faultactor", Namespace = "")]
        public string Actor { get; set; }

        [XmlAnyElement("detail", Namespace = "")]
        public XElement Detail { get; set; }
    }
}