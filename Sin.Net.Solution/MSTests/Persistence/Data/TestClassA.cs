using System;

namespace MSTests.Persistence.Data
{
    internal class TestClassA : ITestable
    {
        public TestClassA()
        {
            Name = "A";
            Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public string Id { get; set; }
    }
}
