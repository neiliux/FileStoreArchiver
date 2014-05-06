using System.Collections.Generic;

namespace FSA.DTO
{
	/// <summary>
	/// Provides wrapper around a configuration section
	/// </summary>
	/// <remarks></remarks>
	public class FsaConfigurationSection
	{
		/// <summary>
		/// Gets configuration attributes.
		/// </summary>
		/// <remarks></remarks>
		public IEnumerable<FsaConfigurationAttribute> Attributes { get; private set; }

		/// <summary>
		/// Gets configuration values.
		/// </summary>
		/// <remarks></remarks>
		public IEnumerable<FsaConfigurationValue> Values { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationSection"/> class.
		/// </summary>
		/// <param name="attributes">The attributes.</param>
		/// <param name="values">The values.</param>
		/// <remarks></remarks>
		public FsaConfigurationSection(
			IEnumerable<FsaConfigurationAttribute> attributes,
			IEnumerable<FsaConfigurationValue> values)
		{
			Attributes = attributes;
			Values = values;
		}
	}
}