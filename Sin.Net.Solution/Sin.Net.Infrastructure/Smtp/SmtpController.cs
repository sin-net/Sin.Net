using Sin.Net.Domain.Config;
using Sin.Net.Domain.Infrastructure;
using Sin.Net.Domain.Infrastructure.Smtp;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
			_messages.Clear();
			IsConnected = true;
			return this;
		}

		public IControlable Disconnect()
		{
			_messages.Clear();
			IsConnected = false;
			return this;
		}

		public ISmtpControlable BuildMessage(string subject, string body)
		{
			var from = Config.Sender;
			foreach (var to in Config.Receivers)
			{
				AddMessage(from, to, subject, body);
			}
			return this;
		}

		public ISmtpControlable BuildMessage(string sender, string receiver, string subject, string body)
		{
			AddMessage(sender, receiver, subject, body);
			return this;
		}

		public ISmtpControlable BuildMessages(string sender, string[] receivers, string subject, string body)
		{
			foreach (var to in receivers)
			{
				AddMessage(sender, to, subject, body);
			}
			return this;
		}

		private void AddMessage(string from, string to, string subject, string body)
		{
			var msg = new MailMessage(from, to, subject, body);
			_messages.Add(msg);
		}

		public ISmtpControlable Attach(string filename)
		{
			return Attach(filename, MediaTypeNames.Text.Plain);
		}

		public ISmtpControlable Attach(FileInfo file)
		{
			return Attach(file.FullName, MediaTypeNames.Text.Plain);
		}

		public ISmtpControlable Attach(FileInfo file, string mimeType)
		{
			return Attach(file.FullName, mimeType);
		}

		public ISmtpControlable Attach(string filename, string mimeType)
		{
			var attachment = new Attachment(filename, mimeType);
			_messages.Last().Attachments.Add(attachment);
			Log.Debug($"The file '{filename}' was added as attachment to the current email.");
			return this;
		}

		public bool Send()
		{
			var success = true;
			SmtpClient client = null;
			try
			{
				client = new SmtpClient(Config.Name, Config.Port)
				{
					UseDefaultCredentials = false,
					EnableSsl = true,
					Credentials = new NetworkCredential(Config.Credentials.Name, Config.Credentials.Password)
				};

				int attempt = 1;
				int limit = 3;
				while(_messages.Count > 0 && limit > 0)
				{
					var mail = _messages.First();
					try
					{
						client.Send(mail);
						_messages.Remove(mail);
						Counter++;
					}
					catch (Exception ex)
					{
						Log.Error($"Attempt {attempt} failed sending email via {client.Host}.");
						Log.Fatal(ex);
						limit--;
						attempt++;
						success = false;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Fatal(ex);
				success = false;
			}
			finally
			{
				Log.Debug($"{Counter} messages were sent since the last setup.");
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
