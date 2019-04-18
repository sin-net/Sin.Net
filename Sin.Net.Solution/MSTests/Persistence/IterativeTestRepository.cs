using Sin.Net.Domain.Repository;

namespace MSTests.Persistence
{
    class IterativeTestRepository : IterativeRepositoryBase<double>
    {
        public IterativeTestRepository()
        {
            Name = "text-repo";
        }

        public int MyProperty { get; set; }
    }
}
