using System.Diagnostics;
using System.Xml.Linq;
using FSA.DAL;
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
		/// <summary>
		/// DAL provider.
		/// </summary>
		private readonly IFsaConfigurationDal _fsaConfigurationDal;

		/// <summary>
		/// XSD based data validator
		/// </summary>
		private readonly IFsaConfigurationValidator _fsaConfigurationValidator;

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationDataProvider"/> class.
		/// </summary>
		/// <remarks></remarks>
		public FsaConfigurationDataProvider()
		{
			_fsaConfigurationDal = new FsaConfigurationDal();
			_fsaConfigurationValidator = new FsaConfigurationValidator();
		}	

#if DEBUG
		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationDataProvider"/> class.
		/// </summary>
		/// <param name="fsaConfigurationDal">The fsa configuration dal.</param>
		/// <param name="fsaConfigurationValidator">The fsa configuration validator </param>
		/// <remarks></remarks>
		public FsaConfigurationDataProvider(IFsaConfigurationDal fsaConfigurationDal, IFsaConfigurationValidator fsaConfigurationValidator)
		{
			_fsaConfigurationDal = fsaConfigurationDal;
			_fsaConfigurationValidator = fsaConfigurationValidator;
		}
#endif

		/// <summary>
		/// Gets the configuration XML.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public XDocument GetConfigurationXml(FsaConfigurationLoadOptions options)
		{
			Debug.Assert(options != null);
			Debug.Assert(!string.IsNullOrWhiteSpace(options.PathToConfigXmlFile));

			XDocument configurationXml = _fsaConfigurationDal.GetConfigurationXml(options.PathToConfigXmlFile);
			_fsaConfigurationValidator.ValidateConfiguration(configurationXml);
			return configurationXml;
		}
	}
}