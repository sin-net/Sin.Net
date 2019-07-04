using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sin.Net.Domain.System.Security
{
    /// <summary>
    /// This extension class defines methods for security checks in combination with the attribute class 'SecurityLevelAttribute'.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Extension method for security checks. First the method extracts the attributes of the member and
        /// then checks the values with the current level.
        /// </summary>
        /// <param name="obj">The calling object</param>
        /// <param name="currentLevel">the current level of the application or user.</param>
        /// <param name="memberName">the calling member (method or property).</param>
        /// <returns></returns>
        public static bool SecureAccess(this object obj, int currentLevel, [CallerMemberName] string memberName = "")
        {
            object[] attributes = null;
            MethodBase method = obj.GetType().GetMethod(memberName);
            PropertyInfo prop = obj.GetType().GetProperty(memberName);

            if (method != null)
            {
                attributes = method.GetCustomAttributes(typeof(SecurityLevelAttribute), true);
            }
            else if (prop != null)
            {
                attributes = prop.GetCustomAttributes(typeof(SecurityLevelAttribute), true);
            }
            else
            {
                Log.Trace($"no property or method found in '{obj.ToString()} that fits to '{memberName}'");
                return true;
            }

            if (attributes == null || attributes.Length != 1)
            {
                Log.Trace($"no security level applied in '{memberName}'");
                return true;
            }

            // security check
            return ValidateLevel(currentLevel, (SecurityLevelAttribute)attributes[0], memberName);
        }

        /// <summary>
        /// Checks if the current level is at least the same level that was set up at the calling member
        /// </summary>
        /// <param name="currentLevel">the current level of the application or user.</param>
        /// <param name="security">the security attributes form the calling member.</param>
        /// <param name="memberName">the calling member (method or property).</param>
        /// <returns></returns>
        private static bool ValidateLevel(int currentLevel, SecurityLevelAttribute security, string memberName)
        {
            if (currentLevel < security.Level)
            {
                // not passed
                string msg;
                if (string.IsNullOrEmpty(security.Message))
                {
                    msg = $"Cannot perform the function: The current security level ({currentLevel}) does not fulfill the requirements ({security.Level})";
                }
                else
                {
                    msg = security.Message;
                }

                Log.Trace($"Cannot perform '{memberName}': The current security level ({currentLevel}) does not fulfill the requirements ({security.Level})");
                if (security.Throw)
                {
                    throw new MethodAccessException(msg);
                }
                else
                {
                    Log.Error(msg);
                }
                return false;
            }
            else
            {
                // passed
                Log.Trace($"security check for {memberName} passed");
                return true;
            }
        }
    }
}
