using System;
using System.Collections.Generic;
using System.Text;
using Sin.Net.Domain.Config;

namespace Sin.Net.Domain.Infrastructure.Smtp
{
	public interface ISmtpControlable : IControlable
	{
		ISmtpControlable BuildMessage(
			string subject,
			string body,
			object[] attachment = null);

		ISmtpControlable BuildMessage(
			string sender,
			string receiver,
			string subject,
			string body,
			object[] attachment = null);

		ISmtpControlable BuildMessages(
			string sender,
			string[] receivers,
			string subject,
			string body,
			object[] attachment = null);

		bool Send();

		int Counter { get; }
	}
}
