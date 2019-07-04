namespace Sin.Net.Domain.Enumerations
{
    /// <summary>
    /// Enum for setting readable security levels.
    /// The values are castet into integers to use them by the security mechanism.
    /// </summary>
    public enum SecurityLevels
    {
        /// <summary>
        /// Level 0 with lowest access rights.
        /// </summary>
        Guest = 0,
        /// <summary>
        /// Level 1 with low access rights.
        /// </summary>
        User = 1,
        /// <summary>
        /// Level 2 with high access rights.
        /// </summary>
        Expert = 2,
        /// <summary>
        /// Level 3 with highest access rights.
        /// </summary>
        Admin = 3
    }
}
