using System.Xml.Linq;
using FSA.DTO;

namespace FSA.Interfaces
{
	/// <summary>
	/// Provides API for getting raw FSA configuration data
	/// </summary>
	public interface IFsaConfigurationDataProvider
	{
		/// <summary>
		/// Gets the configuration XML.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		XDocument GetConfigurationXml(FsaConfigurationLoadOptions options);
	}
}