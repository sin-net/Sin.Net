using Sin.Net.Domain.Config;
using System.Collections.Generic;

namespace Sin.Net.Infrastructure.Mqtt
{
    public class MqttConfig : ConfigBase
    {
        // fields

        private int _qos;

        // -- constructor

        public MqttConfig() : base("mqtt-config")
        {
            Broker = "broker.hivemq.com";
            Port = 1883;
            ClientID = "Sin.Net-Mqtt-Client";
            LasWill = $"Goodby from {ClientID}";
            QoS = 0;
            Topics = new List<string>();
        }

        // -- methods

        public override string ToString() => $"{ClientID} @ {Broker}:{Port}";

        // -- properties

        public string Broker { get; set; }
        public int Port { get; set; }
        public string ClientID { get; set; }

        /// <summary>
        /// Message to be published by the client when he disconnects
        /// </summary>
        public string LasWill { get; set; }

        /// <summary>
        /// Gets or sets the quality of service value [0..2]
        /// </summary>
        public int QoS
        {
            get { return _qos; }
            set
            {
                if (value < 0) _qos = 0;
                else if (value > 2) _qos = 2;
                else _qos = value;
            }
        }

        /// <summary>
        /// Gets or sets the flag wheather the controller should reconnect or not on a connection loss.
        /// </summary>
        public bool AutoReconnect { get; set; }

        /// <summary>
        /// Gets or sets the list of topic strings.
        /// </summary>
        public List<string> Topics { get; set; }

    }
}
