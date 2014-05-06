using System.Xml.Linq;

namespace FSA.Interfaces.BL
{
	/// <summary>
	/// Provides API for validation of FSA configuration data
	/// </summary>
	/// <remarks></remarks>
	public interface IFsaConfigurationValidator
	{
		/// <summary>
		/// Validates a configuration block.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <remarks></remarks>
		void ValidateConfiguration(XDocument configuration);
	}
}