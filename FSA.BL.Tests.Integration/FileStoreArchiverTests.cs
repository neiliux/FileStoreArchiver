using FSA.Interfaces;
using FSA.Tests.Shared;
using NUnit.Framework;

namespace FSA.BL.Tests.Integration
{
	[TestFixture]
	[Category("Integration")]
	public class FileStoreArchiverTests : DebugAssertMockedTestFixture
	{
		private IFileStoreArchiver _fileStoreArchiver;

		[SetUp]
		public void Setup()
		{
			_fileStoreArchiver = FileStoreArchiver.Instance;
		}

		[Test]
		public void GetInstance_ExpectStaticCtorToInstansiateObject()
		{
			// This is intentionally left empty as we want to verify
			// that the code in the Setup function did it's job.
		}
	}
}