﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sin.Net.Persistence.IO.Json
{
    public class LowerCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.First().ToString().ToLower() + string.Join("", propertyName.Skip(1));
        }
    }
}
