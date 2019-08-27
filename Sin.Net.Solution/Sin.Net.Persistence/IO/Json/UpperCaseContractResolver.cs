using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sin.Net.Persistence.IO.Json
{
    public class UpperCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.First().ToString().ToUpper() + string.Join("", propertyName.Skip(1));
        }
    }
}
