using System;

namespace MSTests.Persistence.Data
{
    internal class TestClassB : ITestable
    {
        public TestClassB()
        {
            Name = "B";
            Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public string Id { get; set; }
    }
}
