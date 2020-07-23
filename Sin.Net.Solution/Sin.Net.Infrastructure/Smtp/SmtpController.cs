using Sin.Net.Domain.Config;
using Sin.Net.Domain.Infrastructure;
using Sin.Net.Domain.Infrastructure.Smtp;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Sin.Net.Infrastructure.Smtp
{
    public class SmtpController : ISmtpControlable
    {
        private List<MailMessage> _messages;

        public SmtpController()
        {
            _messages = new List<MailMessage>();
        }

        // -- methods

        IControlable IControlable.Setup(ConfigBase config)
        {
            return Setup(config as SmtpConfig);
        }

        public SmtpController Setup(SmtpConfig config)
        {
            Config = config;
            _messages.Clear();
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

        public ISmtpControlable BuildMessage(string subject, string body, object[] attachments = null)
        {
            var from = Config.Sender;
            foreach(var to in Config.Receivers)
			{
                AddMessage(from, to, subject, body, attachments);
            }
            return this;
        }

        public ISmtpControlable BuildMessage(string sender, string receiver, string subject, string body, object[] attachments = null)
        {
            AddMessage(sender, receiver, subject, body, attachments);
            return this;
        }

        public ISmtpControlable BuildMessages(string sender, string[] receivers, string subject, string body, object[] attachments = null)
        {
            foreach(var to in receivers)
			{
                AddMessage(sender, to, subject, body, attachments);
            }
            return this;
        }

        private void AddMessage(string from, string to, string subject, string body, object[] attachments = null)
		{
            var msg = new MailMessage(from, to, subject, body);
            // TODO: handle attachments
            _messages.Add(msg);
        }

        public bool Send()
        {
            var success = false;
            SmtpClient client = null;
            try
            {
				client = new SmtpClient(Config.Name, Config.Port)
				{
					UseDefaultCredentials = false,
					EnableSsl = true,
					Credentials = new NetworkCredential(Config.Credentials.Name, Config.Credentials.Password)
				};

				foreach (var mail in _messages)
                {                    
                    client.Send(mail);
                    Counter++;
                }
                success = true;
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

            return success;
        }

		// -- properties

		public bool IsConnected { get; private set; }

        ConfigBase IControlable.Config => Config;

        public SmtpConfig Config { get; private set; }

		public int Counter { get; private set; }
	}
}
