using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Tests.Shared;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace FSA.BL.Tests.Unit
{
	[TestFixture]
	[Category("Unit")]
	public class FsaConfigurationManager : DebugAssertMockedTestFixture
	{
		private IFsaConfigurationManager _fsaConfigurationManager;
		private Mock<IFsaConfigurationDataProvider> _mockedFsaConfigurationDataProvider;

		[SetUp]
		public void Setup()
		{
			_mockedFsaConfigurationDataProvider = new Mock<IFsaConfigurationDataProvider>();
			_fsaConfigurationManager = new BL.FsaConfigurationManager(_mockedFsaConfigurationDataProvider.Object);
		}

		[TearDown]
		public void Cleanup()
		{
			_mockedFsaConfigurationDataProvider = null;
			_fsaConfigurationManager = null;
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void GetConfiguration_NullOptions_AssertFailure()
		{
			_fsaConfigurationManager.GetConfiguration(null);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void GetConfiguration_DataProviderReturnsNull_AssertFailure()
		{
			_mockedFsaConfigurationDataProvider.Setup(d => d.GetConfigurationXml(new FsaConfigurationLoadOptions())).Returns((XDocument)null);
			_fsaConfigurationManager.GetConfiguration(new FsaConfigurationLoadOptions());
		}

		[Test]
		public void GetConfiguration_ReturnedConfigurationIncludingEmailSummaryDetails_EmailSummaryDetailsIsReturned()
		{
			const string enabledFlag = "true";
			const string smtpServer = "a.a.a.a";
			const string fromAddress = "a@a.com";

			var configurationXml = ConfigurationSamples.CreateConfigurationWithDynamicEmailSummaryValues(enabledFlag, smtpServer, fromAddress);
			_mockedFsaConfigurationDataProvider.Setup(d => d.GetConfigurationXml(It.IsAny<FsaConfigurationLoadOptions>())).Returns(configurationXml);

			FsaConfiguration configuration = _fsaConfigurationManager.GetConfiguration(new FsaConfigurationLoadOptions());
			Assert.IsNotNull(configuration);
			Assert.IsNotNull(configuration.EmailSummaryDetails);
		}

		[Test]
		public void GetConfiguration_ReturnedConfigurationIncludesEmailSummaryDetails_ValidEmailDetailsReturned()
		{
			const bool enabledFlag = true;
			const string smtpServer = "a.a.a.a";
			const string fromAddress = "a@a.com";

			var configurationXml = ConfigurationSamples.CreateConfigurationWithDynamicEmailSummaryValues(enabledFlag.ToString(CultureInfo.InvariantCulture), 
																											smtpServer, fromAddress);

			_mockedFsaConfigurationDataProvider.Setup(d => d.GetConfigurationXml(It.IsAny<FsaConfigurationLoadOptions>()))
									.Returns(configurationXml);

			FsaConfiguration configuration = _fsaConfigurationManager.GetConfiguration(new FsaConfigurationLoadOptions());
			
			Assert.AreEqual(configuration.EmailSummaryDetails.Enabled, enabledFlag);
			Assert.AreEqual(configuration.EmailSummaryDetails.SmtpServer, smtpServer);
			Assert.AreEqual(configuration.EmailSummaryDetails.FromAddress, fromAddress);
		}

		[Test]
		public void GetConfiguration_ReturnedConfigurationIncludesFileStoreDetails_FileStoreDetailsAreReturned()
		{
			var configurationXml = ConfigurationSamples.CreateConfigurationWithValidFileStoreNode();

			_mockedFsaConfigurationDataProvider.Setup(d => d.GetConfigurationXml(It.IsAny<FsaConfigurationLoadOptions>()))
								.Returns(configurationXml);

			FsaConfiguration configuration = _fsaConfigurationManager.GetConfiguration(new FsaConfigurationLoadOptions());
			Assert.IsNotNull(configuration);
			Assert.IsNotNull(configuration.FileStores);
			Assert.IsTrue(configuration.FileStores.Any());
		}

		[Test]
		public void GetConfiguration_ReturnedConfigurationIncludesMultipleFileStoreDetails_CorrectNumberOfFileStoreDetailsAreReturned()
		{
			var configurationXml = ConfigurationSamples.CreateConfigurationWithThreeValidFileStoreNodes();

			_mockedFsaConfigurationDataProvider.Setup(d => d.GetConfigurationXml(It.IsAny<FsaConfigurationLoadOptions>()))
								.Returns(configurationXml);

			FsaConfiguration configuration = _fsaConfigurationManager.GetConfiguration(new FsaConfigurationLoadOptions());
			Assert.IsNotNull(configuration);
			Assert.IsNotNull(configuration.FileStores.Count() == 3);
		}
	}
}