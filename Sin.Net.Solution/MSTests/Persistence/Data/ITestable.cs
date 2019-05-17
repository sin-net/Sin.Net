using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Persistence.Data
{
    internal interface ITestable
    {
        string Name { get; set; }

        string Id { get; set; }
    }
}
