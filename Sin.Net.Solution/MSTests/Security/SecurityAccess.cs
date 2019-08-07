using Sin.Net.Domain.Enumerations;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.System.Security;
using System.Runtime.CompilerServices;

namespace MSTests.Security
{
    public class SecurityAccess : ISecurable
    {
        private int _userSecurityLevel;
        private float _someFloat;

        public SecurityAccess(int userSecurityLevel)
        {
            _userSecurityLevel = userSecurityLevel;
            Passed = false;
        }

        // -- methods

        [SecurityLevel(Level = 1, Message = "you did not passed level one", Throw = true)]
        public void AccessLevelOne()
        {
            Secure();
            Log.Info("calling SecureLevelOne() passed");
            Passed = true;
        }

        [SecurityLevel(Level = 2, Message = "you did not passed level two", Throw = true)]
        public void AccessLevelTwo()
        {
            Secure();
            Log.Info("calling SecureLevelTwo() passed");
            Passed = true;
        }

        // -- interface

        public bool Secure([CallerMemberName] string memberName = "")
        {
            Passed = false;
            return this.SecureAccess((SecurityLevels)_userSecurityLevel, memberName);
        }

        // -- properties


        [SecurityLevel(1, Message = "No allowed to change SomeFloat", Throw = true)]
        public float SomeFloat
        {
            get
            {
                return _someFloat;
            }
            set
            {
                Secure();
                Passed = true;
                _someFloat = value;
            }
        }

        /// <summary>
        /// !!! Attention !!! do not use the SecurityLevel attribute this way!
        /// The Secure method can't distinguish between a calling getter or setter.
        /// </summary>
        public string SomeString
        {
            [SecurityLevel(1, Throw = true)]
            get
            {
                Secure();
                return "123";
            }
            [SecurityLevel(2, Message = "No allowed to change SomeString", Throw = true)]
            set
            {
                Secure();
                var s = value;
            }
        }

        public bool Passed { get; set; }
    }
}
