using System.Security;

namespace Sin.Net.Domain.System.Security
{
	public class SimpleCredentials
	{

		// -- constructors

		public SimpleCredentials()
		{

		}

		public SimpleCredentials(string name, string password) : this()
		{
			Name = name;
			SetPassword(password);
		}

		public SimpleCredentials(string name, SecureString password) : this()
		{
			Name = name;
			Password = password;
		}

		// -- methods

		public void SetPassword(in string password)
		{
			unsafe
			{
				fixed (char* ptr = password)
				{
					this.Password = new SecureString(ptr, password.Length);
				}
			}
		}

		// -- properties

		public string Name { get; set; }

		public SecureString Password { get; private set; }
	}
}
