using System.Diagnostics;
using System.Xml.Linq;
using FSA.Interfaces.DAL;

namespace FSA.DAL
{
	/// <summary>
	/// FSA Configuration DAL
	/// </summary>
	/// <remarks></remarks>
	public class FsaConfigurationDal : IFsaConfigurationDal
	{
		/// <summary>
		/// Gets the configuration XML.
		/// </summary>
		/// <param name="pathToConfigurationFile">The path to configuration file.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public XDocument GetConfigurationXml(string pathToConfigurationFile)
		{
			Debug.Assert(!string.IsNullOrWhiteSpace(pathToConfigurationFile));
			return XDocument.Load(pathToConfigurationFile);
		}
	}
}