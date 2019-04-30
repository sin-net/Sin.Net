using Sin.Net.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Persistence
{
    class TestRepository : RepositoryBase<double>
    {
        public TestRepository() : base()
        {
            Name = "test repo without enumerator";
        }

        // -- properties

        public string MyProperty { get; set; }
    }
}
