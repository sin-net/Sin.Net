using Sin.Net.Domain.Config;
using System.Collections.Generic;

namespace Sin.Net.Infrastructure.Smtp
{
    public class SmtpConfig : ConfigBase
    {

        public SmtpConfig(string name) : base(name)
        {
            Receivers = new List<string>();
        }

		// -- properties

		public int Port { get; set; }

		public string Sender { get; set; }

		public List<string> Receivers { get; set; }

		public SmtpCredentials Credentials { get; set; }

		// -- inner classes

		public class SmtpCredentials
		{
			public string Name { get; set; }

			public string Password { get; set; }
		}

	}
}
