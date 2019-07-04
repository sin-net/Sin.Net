using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.Net.Domain.System.Security
{
    /// <summary>
    /// Attribute class to describe the access for methods or properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class SecurityLevelAttribute : Attribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SecurityLevelAttribute()
        {
            Level = 0;  // guest
            Throw = true;
        }

        /// <summary>
        /// Constructor with specific minimum security level
        /// aktiviertem Auslösen einer Ausnahme bei Verletzung der Attribute.
        /// </summary>
        /// <param name="level"></param>
        public SecurityLevelAttribute(int level) : this ()
        {
            Level = level;
        }

        // -- properties

        /// <summary>
        /// Gets or sets the minimum security level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets a boolean flag wheather the security mechanism will throw an exception or not.
        /// </summary>
        public bool Throw { get; set; }

        /// <summary>
        /// Gets or sets the message that will be used, when the security mechanism will throw an exception.
        /// </summary>
        public string Message { get; set; }
    }
}
