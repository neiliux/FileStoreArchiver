using System.Xml.Linq;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Interfaces.BL;
using FSA.Interfaces.DAL;
using FSA.Tests.Shared;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
#if DEBUG
namespace FSA.BL.Tests.Unit
{
	[TestFixture]
	public class FsaConfigurationDataProvider : DebugAssertMockedTestClass
	{
		private IFsaConfigurationDataProvider _dataProvider;
		private Mock<IFsaConfigurationDal> _fsaConfigurationDalMock;
		private Mock<IFsaConfigurationValidator> _fsaConfigurationValidatorMock;

		[SetUp]
		public void TestSetup()
		{
			_fsaConfigurationDalMock = new Mock<IFsaConfigurationDal>();
			_fsaConfigurationValidatorMock = new Mock<IFsaConfigurationValidator>();
			_dataProvider = new BL.FsaConfigurationDataProvider(_fsaConfigurationDalMock.Object, _fsaConfigurationValidatorMock.Object);
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
		public void GetConfigurationXml_NullPathInOptions_AssertFailure()
		{
			FsaConfigurationLoadOptions options = 
				new FsaConfigurationLoadOptions
					{
						PathToConfigXmlFile = null
					};

			_dataProvider.GetConfigurationXml(options);
		}

		[Test]
		public void GetConfigurationXml_ValidOptions_ReturnsConfigurationXml()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());

			FsaConfigurationLoadOptions options =
				new FsaConfigurationLoadOptions
					{
						PathToConfigXmlFile = "fakePath"
					};

			XDocument configurationXml = _dataProvider.GetConfigurationXml(options);
			Assert.IsNotNull(configurationXml);
		}

		[Test]
		public void GetConfigurationXml_ValidOptions_InvokesDalCall()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());

			FsaConfigurationLoadOptions options =
				new FsaConfigurationLoadOptions
				{
					PathToConfigXmlFile = "fakePath"
				};

			_dataProvider.GetConfigurationXml(options);
			_fsaConfigurationDalMock.Verify(dal => dal.GetConfigurationXml(It.IsAny<string>()), Times.Once());
		}

		[Test]
		public void GetConfigurationXml_ValidOptions_InvokesValidatorCall()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());
			_fsaConfigurationValidatorMock.Setup(bl => bl.ValidateConfiguration(It.IsAny<XDocument>()));

			FsaConfigurationLoadOptions options =
				new FsaConfigurationLoadOptions
				{
					PathToConfigXmlFile = "fakePath"
				};

			_dataProvider.GetConfigurationXml(options);
			_fsaConfigurationValidatorMock.Verify(bl => bl.ValidateConfiguration(It.IsAny<XDocument>()), Times.Once());
		}

		[Test]
		[ExpectedException(typeof(FsaConfigurationXsdException))]
		public void GetConfigurationXml_ValidOptionsButValidatorFails_ExceptionBubblesUp()
		{
			_fsaConfigurationDalMock.Setup(dal => dal.GetConfigurationXml(It.IsAny<string>())).Returns(new XDocument());
			_fsaConfigurationValidatorMock.Setup(bl => bl.ValidateConfiguration(It.IsAny<XDocument>())).Throws<FsaConfigurationXsdException>();

			FsaConfigurationLoadOptions options =
				new FsaConfigurationLoadOptions
				{
					PathToConfigXmlFile = "fakePath"
				};

			_dataProvider.GetConfigurationXml(options);
		}
	}
}
#endif
// ReSharper restore InconsistentNaming