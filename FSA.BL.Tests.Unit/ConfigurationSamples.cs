using System.Xml.Linq;

namespace FSA.BL.Tests.Unit
{
	/// <summary>
	/// Container for various FSA configuration blocks.
	/// </summary>
	public static class ConfigurationSamples
	{
		public static XDocument CreateMinimumValidConfiguration()
		{
			return new XDocument(new XElement("fsaConfiguration",
													new XElement("configuration",
														new XElement("emailSummary",
															new XAttribute("enabled", true),
															new XElement("smtpServer", "smtp.comcast.net"),
															new XElement("fromAddress", "neiliux@gmail.com")))));
		}

		public static XDocument CreateMinimumInvalidConfiguration()
		{
			return new XDocument(new XElement("invalidRootNode"));
		}

		public static XDocument CreateInvalidConfigurationWithTwoConfigurationElements()
		{
			return new XDocument(new XElement("fsaConfiguration",
													new XElement("configuration"),
													new XElement("configuration")));
		}

		public static XDocument CreateConfigurationWithDynamicEmailSummaryValues(string enabledAttribute, string smtpServerValue, string fromAddressValue)
		{
			return new XDocument(new XElement("fsaConfiguration",
													new XElement("configuration",
														new XElement("emailSummary",
															new XAttribute("enabled", enabledAttribute),
															new XElement("smtpServer", smtpServerValue),
															new XElement("fromAddress", fromAddressValue)))));
		}

		public static XDocument CreateConfigurationWithValidFileStoreNode()
		{
			return new XDocument(new XElement("fsaConfiguration",
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp"))));
		}

		public static XDocument CreateConfigurationWithThreeValidFileStoreNodes()
		{
			return new XDocument(new XElement("fsaConfiguration",
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp")),
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp")),
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp"))));
		}

		public static XDocument CreateConfigurationWithInValidFileStoreNode()
		{
			return new XDocument(new XElement("fsaConfiguration",
											new XElement("fileStore",
												new XElement("methodInvalid", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp"))));
		}

		public static XDocument CreateConfigurationWithMultipleValidFileStoreNode()
		{
			return new XDocument(new XElement("fsaConfiguration",
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp")),
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp")),
											new XElement("fileStore",
												new XElement("method", "copy"),
												new XElement("sourceDirectory", @"c:\temp"),
												new XElement("destinationDirectory", @"c:\temp"))));
		}
	}
}