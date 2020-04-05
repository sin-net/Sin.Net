using Sin.Net.Domain.Config;
using System.Threading.Tasks;

namespace Sin.Net.Domain.Infrastructure.Mqtt
{
    public interface IMqttControlable : IAsyncControlable
    {
        // -- events

        event MqttMessageReceivedEventHandler MessageReceived;
        event MqttConnectedEventHandler Connected;
        event MqttConnectedEventHandler Disconnected;

        // -- methods

        Task PublishAsync(string topic, string message);
        Task PublishAsync(string topic, string message, int qos);
       
    }
}
