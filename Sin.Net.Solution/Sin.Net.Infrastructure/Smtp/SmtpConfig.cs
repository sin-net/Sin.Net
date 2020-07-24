using Sin.Net.Domain.Config;
using Sin.Net.Domain.System.Security;
using System.Collections.Generic;
using System.Security;

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

		public SimpleCredentials Credentials { get; set; }

	}
}
