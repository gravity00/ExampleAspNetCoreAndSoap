using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AspNetCoreMvcSoapController.Soap.V11
{
    public class SoapV11ActionAttribute : Attribute, IActionConstraint
    {
        public bool Accept(ActionConstraintContext context)
        {
            const StringComparison ignoreCase = StringComparison.InvariantCultureIgnoreCase;
            var request = context.RouteContext.HttpContext.Request;

            return "POST".Equals(request.Method, ignoreCase) &&
                   "text/xml".Equals(request.ContentType, ignoreCase) &&
                   request.Headers.Any(e => "SOAPAction".Equals(e.Key, ignoreCase));
        }

        public int Order => 0;
    }
}