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

        // -- methods

        public override string ToString() => Name;

        // -- properties

        public List<string> Receivers { get; set; }

        public int Port { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

    }
}
