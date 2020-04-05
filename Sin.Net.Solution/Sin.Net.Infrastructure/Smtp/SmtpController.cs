using Sin.Net.Domain.Config;
using Sin.Net.Domain.Infrastructure;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Net;
using System.Net.Mail;

namespace Sin.Net.Infrastructure.Smtp
{
    public class SmtpController : IControlable
    {
        public SmtpController()
        {
        }

        // -- methods

        IControlable IControlable.Setup(ConfigBase config)
        {
            return Setup(config as SmtpConfig);
        }

        public SmtpController Setup(SmtpConfig config)
        {
            Config = config;
            Counter = 0;
            return this;
        }

        public IControlable Connect()
        {
            IsConnected = true;
            return this;
        }

        public IControlable Disconnect()
        {
            IsConnected = false;
            return this;
        }

        public SmtpController Send()
        {
            SmtpClient client = null;
            try
            {
                var subject = Config.Subject;
                var body = Config.Body;
                var from = Config.User;

                foreach (var to in Config.Receivers)
                {
                    var msg = new MailMessage(from, to, subject, body);
                    client = new SmtpClient(Config.Name, Config.Port);
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(Config.User, Config.Password);
                    client.Send(msg);
                    Counter++;
                }
            }
            catch(Exception ex)
            {
                Log.Fatal(ex);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }

            return this;
        }

        // -- properties

        public bool IsConnected { get; private set; }

        ConfigBase IControlable.Config => Config;

        public SmtpConfig Config { get; private set; }

        public int Counter { get; private set; }

    }
}
