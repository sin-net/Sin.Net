using Sin.Net.Domain.Config;
using System.Threading.Tasks;

namespace Sin.Net.Domain.Infrastructure.Mqtt
{
    public interface IMqttControlable
    {
        // -- events

        event MqttMessageReceivedEventHandler MessageReceived;
        event MqttConnectedEventHandler Connected;
        event MqttConnectedEventHandler Disconnected;

        // -- methods

        bool CreateClient(ConfigBase config);

        Task PublishAsync(string topic, string message);
        Task PublishAsync(string topic, string message, int qos);
        Task ConnectAsync();
        Task DisconnectAsync();

        // -- properties

        ConfigBase Config { get; }
       
    }
}
