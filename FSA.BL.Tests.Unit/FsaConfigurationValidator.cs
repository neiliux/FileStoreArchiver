using System.Xml.Linq;
using FSA.Interfaces.BL;
using FSA.Tests.Shared;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
#if DEBUG
namespace FSA.BL.Tests.Unit
{
	[TestFixture]
	public class FsaConfigurationValidator : DebugAssertMockedTestClass
	{
		private IFsaConfigurationValidator _fsaConfigurationValidator;

		[SetUp]
		public void TestSetup()
		{
			_fsaConfigurationValidator = new BL.FsaConfigurationValidator();
		}

		[TearDown]
		public void TestTeardown()
		{
			_fsaConfigurationValidator = null;
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void ValidateConfiguration_NullConfigurationXml_ThrowsAssertException()
		{
			_fsaConfigurationValidator.ValidateConfiguration(null);
		}

		[Test]
		public void ValidateConfiguration_MinimumValidConfiguration_ThrowsNoException()
		{
			_fsaConfigurationValidator.ValidateConfiguration(ConfigurationSamples.CreateMinimumValidConfiguration());
		}

		[Test]
		[ExpectedException(typeof(FsaConfigurationXsdException))]
		public void ValidateConfiguration_MinimumInvalidConfiguration_ThrowsValidationException()
		{
			_fsaConfigurationValidator.ValidateConfiguration(ConfigurationSamples.CreateMinimumInvalidConfiguration());
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void GetXsdSchema_NullUri_ThrowsAssertException()
		{
			BL.FsaConfigurationValidator.GetXsdSchema(null);
		}

		[Test]
		public void GetXsdSchema_ValidUri_ThrowsNoExceptions()
		{
			BL.FsaConfigurationValidator.GetXsdSchema(BL.FsaConfigurationValidator.EmbeddedXsdResourceUri);
		}

		[Test]
		[ExpectedException(typeof(FsaConfigurationXsdException))]
		public void ValidateConfiguration_InvalidConfigurationWithTwoConfigurationElements_ThrowsValidationException()
		{
			_fsaConfigurationValidator.ValidateConfiguration(ConfigurationSamples.CreateInvalidConfigurationWithTwoConfigurationElements());
		}

		[Test]
		[ExpectedException(typeof(FsaConfigurationXsdException))]
		public void ValidateConfiguration_InvalidConfigurationWithInvalidEmailEnabledBit_ThrowsValidationException()
		{
			XDocument configuration = ConfigurationSamples.CreateConfigurationWithDynamicEmailSummaryValues("invalidValue", "foo", "bar");
			_fsaConfigurationValidator.ValidateConfiguration(configuration);
		}

		[Test]
		public void ValidateConfiguration_ValidConfigurationWithFileStoreNode_ThrowsNoExceptions()
		{
			_fsaConfigurationValidator.ValidateConfiguration(ConfigurationSamples.CreateConfigurationWithValidFileStoreNode());
		}

		[Test]
		public void ValidateConfiguration_ValidConfigurationWithMultipleFileStoreNodes_ThrowsNoException()
		{
			_fsaConfigurationValidator.ValidateConfiguration(ConfigurationSamples.CreateConfigurationWithMultipleValidFileStoreNode());
		}

		[Test]
		[ExpectedException(typeof(FsaConfigurationXsdException))]
		public void ValidationConfiguration_InvalidConfigurationWithInvalidFileStoreNode_ThrowsValidationException()
		{
			_fsaConfigurationValidator.ValidateConfiguration(ConfigurationSamples.CreateConfigurationWithInValidFileStoreNode());
		}
	}
}
#endif
// ReSharper restore InconsistentNaming