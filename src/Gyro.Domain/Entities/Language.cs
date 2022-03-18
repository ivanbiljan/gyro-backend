using Gyro.Domain.Shared;

namespace Gyro.Domain.Entities
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    public sealed class Language : AuditableEntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Gets or sets the locale associated with this language.
        /// </summary>
        public string Locale { get; set; }
    }
}