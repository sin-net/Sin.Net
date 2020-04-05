using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Infrastructure.Mqtt;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Infrastructure.Mqtt;
using System.Threading.Tasks;

namespace MSTests.Infrastructure.Mqtt
{
    [TestClass]
    public class MqttTests : TestsBase
    {
        IMqttControlable _mqtt;
        private bool _isConnected;
        private MqttConfig _config;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _mqtt = new MqttNetController();
            _config = new MqttConfig
            {
                Broker = "broker.hivemq.com",
                Port = 1883,
                ClientID = "Sin.Net.MSTests"
            };
            _mqtt.Connected += OnConnected;
            _mqtt.Setup(_config);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
            _mqtt.DisconnectAsync();
            _mqtt = null;
        }

        // -- test methods

        [TestMethod]
        public void ConnectAndDisconnectMqtt()
        {
            // connect: act & assert
            _mqtt.Connect();
            Assert.IsTrue(_mqtt.IsConnected, $"mqtt {_mqtt} should be connected");

            // disconnect: act & assert
            _mqtt.Disconnect();
            Assert.IsFalse(_mqtt.IsConnected, $"mqtt {_mqtt} should be disconnected");
        }
        
        [TestMethod]
        public async Task ConnectAndDisconnectMqttAsync()
        {
            // connect: act & assert
            await ConnectAsync();
            Assert.IsTrue(_isConnected, $"mqtt {_mqtt} is not connected");

            // disconnect: act & assert
            await DisconnectAsync();
            Assert.IsFalse(_isConnected);
        }



        // -- private methods

        private async Task ConnectAsync()
        {
            Log.Info($"Connecting with {_config.ToString()}", this);

            // act
            await _mqtt.ConnectAsync();
        }

        private async Task DisconnectAsync()
        {
            Log.Info($"Disconnecting from {_config.ToString()}", this);

            // act
            await _mqtt.DisconnectAsync().ContinueWith((task) =>
            {
                _isConnected = false;
            });
        }

        // -- events

        private void OnConnected(object sender, MqttConnectedEventArgs e)
        {
            Log.Info($"Connected to {e.Broker} with id {e.ClientID}", this);
            _isConnected = true;

            var mqtt = sender as MqttNetController;
            var config = mqtt.Config;
        }
    }
}
