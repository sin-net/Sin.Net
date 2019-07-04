using System.Runtime.CompilerServices;

namespace Sin.Net.Domain.System.Security
{
    /// <summary>
    /// The interface provides a function to secure application layer methods combined with the attribute class SecurityLevelAttribute.
    /// </summary>
    public interface ISecurable
    {
        /// <summary>
        /// Implements arbitrary security checks.
        /// </summary>
        /// <param name="memberName">The name of the calling method, will be set implicitly.</param>
        /// <returns>Returns 'true' when successfull, otherwise 'false'.</returns>
        bool Secure([CallerMemberName] string memberName = "");
    }
}
