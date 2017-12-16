using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AspNetCoreMvcSoapController.Soap.V12
{
    public class SoapV12ActionAttribute : Attribute, IActionConstraint
    {
        private static readonly Regex Regex = new Regex(
            "^(application/soap\\+xml[A-z\\d;=-]{0,}action=\"[A-z\\d_]{0,}\")$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant,
            TimeSpan.FromSeconds(1));

        public bool Accept(ActionConstraintContext context)
        {
            var request = context.RouteContext.HttpContext.Request;

            return "POST".Equals(request.Method, StringComparison.InvariantCultureIgnoreCase) &&
                   Regex.IsMatch(request.ContentType);
        }

        public int Order => 0;
    }
}