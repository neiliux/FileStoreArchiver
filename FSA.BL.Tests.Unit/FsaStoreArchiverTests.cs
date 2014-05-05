using System.Collections.Generic;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Interfaces.BL;
using FSA.Tests.Shared;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
#if DEBUG
namespace FSA.BL.Tests.Unit
{
	[TestFixture]
	public class FsaStoreArchiverTests : DebugAssertMockedTestClass
	{
		private Mock<IFsaConfigurationManager> _configurationManagerMock;
		private Mock<IFsaLogger> _fsaLoggerMock;
		private Mock<IFsaWorkerProvider> _fsaWorkerProviderMock;
		private IFileStoreArchiver _fileStoreArchiver;
		private const string MockPathToConfigurationXml = @"fakePathToConfigFile.xml";

		[SetUp]
		public void TestSetup()
		{
			_configurationManagerMock = new Mock<IFsaConfigurationManager>();
			_fsaWorkerProviderMock = new Mock<IFsaWorkerProvider>();
			_fsaLoggerMock = new Mock<IFsaLogger>();
			_fileStoreArchiver = new FileStoreArchiver(MockPathToConfigurationXml,
													_configurationManagerMock.Object,
													_fsaLoggerMock.Object,
													_fsaWorkerProviderMock.Object);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void RunFsa_NoConfigurationManager_DebugAssertFailure()
		{
			// Overwrite what we set in the TestInitialize method.
			_fileStoreArchiver = new FileStoreArchiver(MockPathToConfigurationXml, null, _fsaLoggerMock.Object, _fsaWorkerProviderMock.Object);
			_fileStoreArchiver.RunFsa();
		}

		[Test]
		public void RunFsa_ValidConfiguration_ExpectResultsObjectsReturned()
		{
			var results = _fileStoreArchiver.RunFsa();
			Assert.IsNotNull(results);
		}

		[Test]
		public void RunFsa_InvalidConfiguration_ExpectResultsObjectWithFailureReturned()
		{
			_configurationManagerMock.Setup(bll => bll.GetConfiguration(It.IsAny<FsaConfigurationLoadOptions>())).Returns((FsaConfiguration)null);
			var results = _fileStoreArchiver.RunFsa();
			Assert.IsNotNull(results.RunException);
		}

		[Test]
		public void RunFsa_ValidSingleWorkerConfiguration_ExpectWorkerToBeInvoked()
		{
			List<FileStoreDetails> fileStoreDetails = new List<FileStoreDetails>
			                                          	{new FileStoreDetails {Method = FileStoreMethod.COPY}};	

			FsaConfiguration fsaConfiguration = new FsaConfiguration(new EmailSummaryDetails(), fileStoreDetails);
			_configurationManagerMock.Setup(bll => bll.GetConfiguration(It.IsAny<FsaConfigurationLoadOptions>())).Returns(fsaConfiguration);	

			Mock<IFsaWorker> fsaWorkerMock = new Mock<IFsaWorker>();
			_fsaWorkerProviderMock.Setup(bll => bll.GetWorker(It.Is<FileStoreDetails>(fsd => fsd == fileStoreDetails[0]))).Returns(fsaWorkerMock.Object);

			_fileStoreArchiver.RunFsa();

			fsaWorkerMock.Verify(worker => worker.Run(It.IsAny<FsaWorkerRunDetails>(), It.IsAny<IFsaLogger>()), Times.Once());
		}

		[Test]
		public void RunFsa_ValidMultipleWorkerConfiguration_ExpectAllWorkersToBeInvoked()
		{
			List<FileStoreDetails> fileStoreDetails = new List<FileStoreDetails> 
														{	
															new FileStoreDetails { Method = FileStoreMethod.COPY },
															new FileStoreDetails { Method = FileStoreMethod.INCREMENTAL }
														};

			FsaConfiguration fsaConfiguration = new FsaConfiguration(new EmailSummaryDetails(), fileStoreDetails);
			_configurationManagerMock.Setup(bll => bll.GetConfiguration(It.IsAny<FsaConfigurationLoadOptions>())).Returns(fsaConfiguration);

			Mock<IFsaWorker> fsaWorker1Mock = new Mock<IFsaWorker>();
			Mock<IFsaWorker> fsaWorker2Mock = new Mock<IFsaWorker>();
			_fsaWorkerProviderMock.Setup(bll => bll.GetWorker(It.Is<FileStoreDetails>(fsd => fsd == fileStoreDetails[0]))).Returns(fsaWorker1Mock.Object);
			_fsaWorkerProviderMock.Setup(bll => bll.GetWorker(It.Is<FileStoreDetails>(fsd => fsd == fileStoreDetails[1]))).Returns(fsaWorker2Mock.Object);

			_fileStoreArchiver.RunFsa();

			fsaWorker1Mock.Verify(worker => worker.Run(It.IsAny<FsaWorkerRunDetails>(), It.IsAny<IFsaLogger>()), Times.Once());
			fsaWorker2Mock.Verify(worker => worker.Run(It.IsAny<FsaWorkerRunDetails>(), It.IsAny<IFsaLogger>()), Times.Once());
		}
	}
}
#endif
// ReSharper restore InconsistentNaming