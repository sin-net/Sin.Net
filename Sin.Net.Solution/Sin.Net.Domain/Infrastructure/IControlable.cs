using Sin.Net.Domain.Config;

namespace Sin.Net.Domain.Infrastructure
{
    public interface IControlable
    {
        IControlable Setup(ConfigBase config);

        IControlable Connect();

        IControlable Disconnect();

        // -- properties

        bool IsConnected { get; }

        ConfigBase Config { get; }
    }
}
