using System.Xml.Linq;

namespace FSA.Interfaces.DAL
{
	/// <summary>
	/// FSA Configuration DAL
	/// </summary>
	public interface IFsaConfigurationDal
	{
		/// <summary>
		/// Gets the configuration XML.
		/// </summary>
		/// <param name="pathToConfigurationFile">The path to configuration file.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		XDocument GetConfigurationXml(string pathToConfigurationFile);
	}
}