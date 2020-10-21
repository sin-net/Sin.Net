using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using Sin.Net.Domain.Config;

namespace Sin.Net.Domain.Infrastructure.Smtp
{
	public interface ISmtpControlable : IControlable
	{
		ISmtpControlable BuildMessage(
			string subject,
			string body);

		ISmtpControlable BuildMessage(
			string sender,
			string receiver,
			string subject,
			string body);

		ISmtpControlable BuildMessages(
			string sender,
			string[] receivers,
			string subject,
			string body);

		ISmtpControlable Attach(string filename);
		ISmtpControlable Attach(string filename, string mimeType);
		ISmtpControlable Attach(FileInfo file);
		ISmtpControlable Attach(FileInfo file, string mimeType);

		bool Send();

		int Counter { get; }
	}
}
