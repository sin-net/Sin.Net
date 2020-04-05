using System.Threading.Tasks;

namespace Sin.Net.Domain.Infrastructure
{
    public interface IAsyncControlable : IControlable
    {
        Task ConnectAsync();

        Task DisconnectAsync();
    }
}
