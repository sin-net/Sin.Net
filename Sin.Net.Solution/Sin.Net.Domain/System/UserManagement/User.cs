using Sin.Net.Domain.Enumerations;

namespace Sin.Net.Domain.System.UserManagement
{
    /// <summary>
    /// User data model for storage in app-config.
    /// </summary>
    public class User
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public User()
        {

        }

        // -- methods

        /// <summary>
        /// Returns the Name property.
        /// </summary>
        /// <returns>string with the Name.</returns>
        public override string ToString() => Name;

        // -- properties

        /// <summary>
        /// gets or sets the system user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the security level for this user.
        /// Higher Levels
        /// </summary>
        public SecurityLevels Level { get; set; }

        /// <summary>
        /// Gets or sets the Email for further use.
        /// </summary>
        public string Email { get; set; }
    }
}
