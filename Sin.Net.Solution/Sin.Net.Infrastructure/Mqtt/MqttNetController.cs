using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Sin.Net.Domain.Config;
using Sin.Net.Domain.Infrastructure;
using Sin.Net.Domain.Infrastructure.Mqtt;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Sin.Net.Infrastructure.Mqtt
{
    public class MqttNetController : IMqttControlable
    {
        // -- fields

        private IMqttClient _client;
        private IMqttClientOptions _options;

        public event MqttMessageReceivedEventHandler MessageReceived;
        public event MqttConnectedEventHandler Connected;
        public event MqttConnectedEventHandler Disconnected;

        // -- constructor

        public MqttNetController()
        {

        }

        // -- methods

        IControlable IControlable.Setup(ConfigBase config)
        {
            return Setup(config as MqttConfig);
        }

        public IControlable Setup(MqttConfig config)
        {
            Config = config;
            _options = new MqttClientOptionsBuilder()
               .WithTcpServer(Config.Broker, Config.Port)
               .WithClientId(Config.ClientID)
               .WithCleanSession(true)
               .Build();

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            _client.UseConnectedHandler(async e =>
            {
                // fire own event for application layer
                if (e.AuthenticateResult.ResultCode != MqttClientConnectResultCode.Success)
                {
                    throw new Exception($"Connection error '{e.AuthenticateResult.ResultCode.ToString()}' in {this.GetType().Name}");
                }

                Connected?.Invoke(this, new MqttConnectedEventArgs(Config.Broker, Config.Port, Config.ClientID));
                // bind all topics to the connected broker
                foreach (string topic in Config.Topics)
                {
                    await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
                }
            });

            _client.UseDisconnectedHandler(async e =>
            {
                Log.Warn($"Disconnected from broker '{Config.Broker}'", this);
                // fire own event for application layer
                Disconnected?.Invoke(this, new MqttConnectedEventArgs(Config.Broker, Config.Port, Config.ClientID));
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await ConnectAsync();
                }
                catch (Exception ex)
                {
                    Log.Error("could not reconnect", this);
                    Log.Fatal(ex);
                }
            });

            _client.UseApplicationMessageReceivedHandler(e =>
            {
                Log.Trace(e.ApplicationMessage.Topic, this);
                string topic = e.ApplicationMessage.Topic;
                string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                MessageReceived?.Invoke(this, new MqttReceivedEventArgs(topic, msg));
            });

            return this;
        }

        public IControlable Connect()
        {
            var t = Task.Run(ConnectAsync);
            t.Wait();
            return this;
        }

        public IControlable Disconnect()
        {
            var t = Task.Run(DisconnectAsync);
            t.Wait();
            return this;
        }

        public async Task ConnectAsync()
        {
            await _client.ConnectAsync(_options);
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }

        public Task PublishAsync(string topic, string message)
        {
            return PublishAsync(topic, message, Config.QoS);
        }

        public async Task PublishAsync(string topic, string message, int qos)
        {
            MqttApplicationMessage msg = new MqttApplicationMessageBuilder()
               .WithTopic(topic)
               .WithPayload(message)
               .WithRetainFlag(false)
               .Build();

            if (qos >= 0 &&
                qos <= 2)
            {
                msg.QualityOfServiceLevel = (MqttQualityOfServiceLevel)qos;
            }
            else
            {
                msg.QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            }

            await _client.PublishAsync(msg);
        }

        // -- properties

        public MqttConfig Config { get; private set; }

        /// <summary>
        /// Gets the information if the controller is connected or not. 
        /// </summary>
        public bool IsConnected => _client?.IsConnected ?? false;

        ConfigBase IControlable.Config => Config;
    }
}
