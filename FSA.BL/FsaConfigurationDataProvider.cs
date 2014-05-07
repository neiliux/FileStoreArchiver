using System.Diagnostics;
using System.Xml.Linq;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Interfaces.BL;
using FSA.Interfaces.DAL;

namespace FSA.BL
{
	/// <summary>
	/// <see cref="IFsaConfigurationDataProvider"/>
	/// </summary>
	public class FsaConfigurationDataProvider : IFsaConfigurationDataProvider
	{
		private readonly IFsaConfigurationDal _fsaConfigurationDal;
		private readonly IFsaConfigurationValidator _fsaConfigurationValidator;
		private readonly IFsaConfigurationFilePathProvider _fsaConfigurationFilePathProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationDataProvider" /> class.
		/// </summary>
		/// <param name="fsaConfigurationDal">The fsa configuration dal.</param>
		/// <param name="fsaConfigurationValidator">The fsa configuration validator</param>
		/// <param name="fsaConfigurationFilePathProvider">The fsa configuration file path provider.</param>
		public FsaConfigurationDataProvider(
			IFsaConfigurationDal fsaConfigurationDal,
			IFsaConfigurationValidator fsaConfigurationValidator,
			IFsaConfigurationFilePathProvider fsaConfigurationFilePathProvider)
		{
			_fsaConfigurationDal = fsaConfigurationDal;
			_fsaConfigurationValidator = fsaConfigurationValidator;
			_fsaConfigurationFilePathProvider = fsaConfigurationFilePathProvider;
		}

		/// <summary>
		/// Gets the configuration XML.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public XDocument GetConfigurationXml(FsaConfigurationLoadOptions options)
		{
			Debug.Assert(options != null);
			string pathToConfigXml = _fsaConfigurationFilePathProvider.GetConfigurationFilePath();
			Debug.Assert(!string.IsNullOrWhiteSpace(pathToConfigXml));

			XDocument configurationXml = _fsaConfigurationDal.GetConfigurationXml(pathToConfigXml);
			_fsaConfigurationValidator.ValidateConfiguration(configurationXml);
			return configurationXml;
		}
	}
}