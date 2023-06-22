using System.Text.RegularExpressions;

namespace AssignmenCheck.API.Extensions
{
    public class ConfigureApiUrlName : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}

