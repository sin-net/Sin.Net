namespace Sin.Net.Domain.Config
{

    /// <summary>
    /// Abstract class to provide basic features for all kinds of config concretions.
    /// </summary>
    public abstract class ConfigBase
    {
        // -- constructors

        /// <summary>
        /// The default constructor that sets the name property.
        /// </summary>
        /// <param name="name">The name value of the config instance will be set as Name property.</param>
        protected ConfigBase(string name)
        {
            Name = name;
        }

        // -- methods

        /// <summary>
        /// Overrwrites the default ToString method with the Name property.
        /// </summary>
        /// <returns>Returns an overwritten string.</returns>
        public override string ToString() => Name;

        // -- properties

        /// <summary>
        /// Gets or sets the name property.
        /// </summary>
        public virtual string Name { get; set; }
    }
}