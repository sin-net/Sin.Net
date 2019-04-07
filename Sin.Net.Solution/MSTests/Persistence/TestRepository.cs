using Sin.Net.Domain.Repository;

namespace MSTests.Persistence
{
    class TestRepository : RepositoryBase<double>
    {
        public TestRepository()
        {
            Name = "text-repo";
        }

        public int MyProperty { get; set; }
    }
}
