using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cben.Domain.Entities.Auditing;
using Cben.MultiTenancy;

namespace Cben.Application.Features
{
    /// <summary>
    /// Base class for feature settings
    /// </summary>
    [Table("CbenFeatures")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class FeatureSetting : CreationAuditedEntity<long>
    {
        /// <summary>
        /// Maximum length of the <see cref="Name"/> field.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// Maximum length of the <see cref="Value"/> property.
        /// </summary>
        public const int MaxValueLength = 2000;

        /// <summary>
        /// Feature name.
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [MaxLength(MaxValueLength)]
        public virtual string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureSetting"/> class.
        /// </summary>
        protected FeatureSetting()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureSetting"/> class.
        /// </summary>
        /// <param name="name">Feature name.</param>
        /// <param name="value">Feature value.</param>
        protected FeatureSetting(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}