using System.Xml.Linq;

namespace FSA.Interfaces.BL
{
	/// <summary>
	/// Provides API for validatation of Fsa Configuration data
	/// </summary>
	/// <remarks></remarks>
	public interface IFsaConfigurationValidator
	{
		/// <summary>
		/// Validates the configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <remarks></remarks>
		void ValidateConfiguration(XDocument configuration);
	}
}