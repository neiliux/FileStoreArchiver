using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using FSA.DTO;
using FSA.Interfaces;

namespace FSA.BL
{
	/// <summary>
	/// Manages FSA configuration.
	/// </summary>
	/// <remarks></remarks>
	public class FsaConfigurationManager : IFsaConfigurationManager
	{
		private readonly IFsaConfigurationDataProvider _dataProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationManager"/> class.
		/// </summary>
		/// <param name="dataProvider">The data provider.</param>
		/// <remarks>CTOR for debug mode.</remarks>
		public FsaConfigurationManager(IFsaConfigurationDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}

		/// <summary>
		/// Retrieves the FSA configuration.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public FsaConfiguration GetConfiguration(FsaConfigurationLoadOptions options)
		{
			Debug.Assert(options != null);

			XDocument configXml = _dataProvider.GetConfigurationXml(options);
			Debug.Assert(configXml != null);

			EmailSummaryDetails emailSummaryDetails = GetEmailSummaryDetailsFromConfiguration(configXml);
			IEnumerable<FileStoreDetails> fileStoreDetails = GetFileStoresDetailsFromConfiguration(configXml);

			return new FsaConfiguration(emailSummaryDetails, fileStoreDetails);
		}

		/// <summary>
		/// Gets the email summary details from configuration.
		/// </summary>
		/// <param name="configXml">The config XML.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static EmailSummaryDetails GetEmailSummaryDetailsFromConfiguration(XDocument configXml)
		{
			XElement configurationNode = GetConfigurationXml(GetFsaConfigurationNode(configXml));
			if (configurationNode == null)
			{
				return null;
			}

			XElement emailSummaryNode = configurationNode.Element("emailSummary");
			Debug.Assert(emailSummaryNode != null);

			EmailSummaryDetails emailSummaryDetails 
				= new EmailSummaryDetails
				{
					Enabled = (bool)emailSummaryNode.Attribute("enabled"),
					FromAddress = (string)emailSummaryNode.Element("fromAddress"),
					SmtpServer = (string)emailSummaryNode.Element("smtpServer")
				};

			return emailSummaryDetails;
		}

		/// <summary>
		/// Gets the file stores details from configuration.
		/// </summary>
		/// <param name="configXml">The config XML.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static IEnumerable<FileStoreDetails> GetFileStoresDetailsFromConfiguration(XDocument configXml)
		{
			List<FileStoreDetails> fileStoreDetails = new List<FileStoreDetails>();
			XElement fsaConfigurationNode = GetFsaConfigurationNode(configXml);
			foreach (XElement fileStore in fsaConfigurationNode.Elements("fileStore"))
			{
				FileStoreDetails fileStoreDetail = CreateFileStoreDetails(fileStore);
				Debug.Assert(fileStoreDetails != null);
				fileStoreDetails.Add(fileStoreDetail);
			}
			return fileStoreDetails;
		}

		/// <summary>
		/// Gets the fsa configuration node.
		/// </summary>
		/// <param name="configXml">The config XML.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static XElement GetFsaConfigurationNode(XDocument configXml)
		{
			XElement fsaConfigurationNode = configXml.Root; //.Element("fsaConfiguration");
			Debug.Assert(fsaConfigurationNode != null);
			return fsaConfigurationNode;
		}

		/// <summary>
		/// Gets the configuration XML.
		/// </summary>
		/// <param name="fsaConfigurationNode">The fsa configuration node.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static XElement GetConfigurationXml(XElement fsaConfigurationNode)
		{
			XElement configurationNode = fsaConfigurationNode.Element("configuration");
			//Debug.Assert(configurationNode != null);
			return configurationNode;
		}

		/// <summary>
		/// Creates the file store details.
		/// </summary>
		/// <param name="fileStoreNode">The file store node.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static FileStoreDetails CreateFileStoreDetails(XElement fileStoreNode)
		{
			FileStoreDetails fileStoreDetails 
				= new FileStoreDetails
				{
					Method = GetFileStoreMethod(fileStoreNode),
					SourceDirectory = (string)fileStoreNode.Element("sourceDirectory"),
					DestinationDirectory = (string)fileStoreNode.Element("destinationDirectory"),
					ConfigurationSection = BuildConfigurationSection(fileStoreNode)
				};

			return fileStoreDetails;
		}

		/// <summary>
		/// Builds the configuration section.
		/// </summary>
		/// <param name="fileStoreNode">The file store node.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static FsaConfigurationSection BuildConfigurationSection(XElement fileStoreNode)
		{
			// TODO: Implement!
			return null;
		}

		/// <summary>
		/// Gets the file store method.
		/// </summary>
		/// <param name="fileStoreNode">The file store node.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static FileStoreMethod GetFileStoreMethod(XElement fileStoreNode)
		{
			string methodString = (string)fileStoreNode.Element("method");
			if (!string.IsNullOrWhiteSpace(methodString))
			{
				methodString = methodString.ToUpper();
			}

			FileStoreMethod method;
			if (!Enum.TryParse(methodString, out method))
			{
				throw new FsaFileStoreMethodNotSupportedException(string.Format("{0} is not supported", methodString));
			}
			return method;
		}
	}
}