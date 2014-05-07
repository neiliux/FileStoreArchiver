using System.Xml.Linq;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Interfaces.BL;
using FSA.Interfaces.DAL;
using FSA.Tests.Shared;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace FSA.BL.Tests.Unit
{
	[TestFixture]
	[Category("Unit")]
	public class FsaConfigurationDataProvider : DebugAssertMockedTestFixture
	{
		private IFsaConfigurationDataProvider _dataProvider;
		private Mock<IFsaConfigurationDal> _fsaConfigurationDalMock;
		private Mock<IFsaConfigurationValidator> _fsaConfigurationValidatorMock;
		private Mock<IFsaConfigurationFilePathProvider> _fsaConfigurationFilePathProviderMock;

		[SetUp]
		public void TestSetup()
		{
			_fsaConfigurationDalMock = new Mock<IFsaConfigurationDal>();
			_fsaConfigurationValidatorMock = new Mock<IFsaConfigurationValidator>();
			_fsaConfigurationFilePathProviderMock = new Mock<IFsaConfigurationFilePathProvider>();
			_dataProvider = new BL.FsaConfigurationDataProvider(
				_fsaConfigurationDalMock.Object,
				_fsaConfigurationValidatorMock.Object,
				_fsaConfigurationFilePathProviderMock.Object);
		}

		[TearDown]
		public void TestCleanup()
		{
			_dataProvider = null;	
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void GetConfigurationXml_NullOptions_AssertFailure()
		{
			_dataProvider.GetConfigurationXml(null);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void GetConfigurationXml_NullFilePath_AssertFailure()
		{
			_fsaConfigurationFilePathProviderMock.Setup(m => m.GetConfigurationFilePath()).Returns((string)null);
			_dataProvider.GetConfigurationXml(new FsaConfigurationLoadOptions());
		}

		[Test]
		public void GetConfigurationXml_ValidOptions_ReturnsConfigurationXml()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());
			_fsaConfigurationFilePathProviderMock.Setup(m => m.GetConfigurationFilePath()).Returns("fakePath");
			
			XDocument configurationXml = _dataProvider.GetConfigurationXml(new FsaConfigurationLoadOptions());
			Assert.IsNotNull(configurationXml);
		}

		[Test]
		public void GetConfigurationXml_ValidOptions_InvokesDalCall()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());
			_fsaConfigurationFilePathProviderMock.Setup(m => m.GetConfigurationFilePath()).Returns("fakePath");

			_dataProvider.GetConfigurationXml(new FsaConfigurationLoadOptions());
			_fsaConfigurationDalMock.Verify(dal => dal.GetConfigurationXml(It.IsAny<string>()), Times.Once());
		}

		[Test]
		public void GetConfigurationXml_ValidOptions_InvokesValidatorCall()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());
			_fsaConfigurationValidatorMock.Setup(bl => bl.ValidateConfiguration(It.IsAny<XDocument>()));
			_fsaConfigurationFilePathProviderMock.Setup(m => m.GetConfigurationFilePath()).Returns("fakePath");

			_dataProvider.GetConfigurationXml(new FsaConfigurationLoadOptions());
			_fsaConfigurationValidatorMock.Verify(bl => bl.ValidateConfiguration(It.IsAny<XDocument>()), Times.Once());
		}

		[Test]
		[ExpectedException(typeof(FsaConfigurationXsdException))]
		public void GetConfigurationXml_ValidOptionsButValidatorFails_ExceptionBubblesUp()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());
			_fsaConfigurationValidatorMock.Setup(bl => bl.ValidateConfiguration(It.IsAny<XDocument>())).Throws<FsaConfigurationXsdException>();
			_fsaConfigurationFilePathProviderMock.Setup(m => m.GetConfigurationFilePath()).Returns("fakePath");

			_dataProvider.GetConfigurationXml(new FsaConfigurationLoadOptions());
		}
	}
}