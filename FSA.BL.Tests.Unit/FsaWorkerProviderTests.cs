using System;
using FSA.DTO;
using FSA.Interfaces.BL;
using FSA.Tests.Shared;
using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
#if DEBUG
namespace FSA.BL.Tests.Unit
{
	[TestFixture]
	public class FsaWorkerProviderTests : DebugAssertMockedTestFixture
	{
		private IFsaWorkerProvider _fsaWorkerProvider;

		[SetUp]
		public void TestSetup()
		{
			_fsaWorkerProvider = new FsaWorkerProvider();
		}

		[ExpectedException(typeof(AssertionException))]
		public void RegisterWorker_PassNull_ExpectDebugAssertException()
		{
			_fsaWorkerProvider.RegisterWorker(null);
		}

		[Test]
		public void RegisterWorker_ValidWorker_ExpectSuccess()
		{
			Mock<IFsaWorker> workerMock = CreateMockedFsaWorker(FileStoreMethod.COPY);
			_fsaWorkerProvider.RegisterWorker(workerMock.Object);
		}

		[Test]
		public void RegisterWorker_RegisterSameWorkTwice_ExpectSuccess()
		{
			Mock<IFsaWorker> workerMock = CreateMockedFsaWorker(FileStoreMethod.COPY);
			_fsaWorkerProvider.RegisterWorker(workerMock.Object);
			_fsaWorkerProvider.RegisterWorker(workerMock.Object);
		}

		[Test]
		public void RegisterWorker_RegisterTwoWorkersWithSameMethod_ExpectLatterRegisteredWorkerToBeReturned()
		{
			const FileStoreMethod workerMethod = FileStoreMethod.COPY;
			var mockedWorker1 = CreateMockedFsaWorker(workerMethod);
			var mockedWorker2 = CreateMockedFsaWorker(workerMethod);

			_fsaWorkerProvider.RegisterWorker(mockedWorker1.Object);
			_fsaWorkerProvider.RegisterWorker(mockedWorker2.Object);

			var returnedWorker = _fsaWorkerProvider.GetWorker(new FileStoreDetails { Method = workerMethod });
			Assert.AreEqual(returnedWorker.ID, mockedWorker2.Object.ID);
		}

		[Test]
		public void GetWorker_ValidMethod_ExpectWorkerReturned()
		{
			const FileStoreMethod workerMethod = FileStoreMethod.COPY;
			RegisterMockedFsaWorker(workerMethod);

			IFsaWorker worker = _fsaWorkerProvider.GetWorker(new FileStoreDetails { Method = workerMethod });
			Assert.IsNotNull(worker);
		}

		private static Mock<IFsaWorker> CreateMockedFsaWorker(FileStoreMethod workerMethod)
		{
			Mock<IFsaWorker> workerMock = new Mock<IFsaWorker>();
			workerMock.Setup(worker => worker.Method).Returns(workerMethod);
			workerMock.Setup(worker => worker.ID).Returns(Guid.NewGuid());
			return workerMock;
		}

		private void RegisterMockedFsaWorker(FileStoreMethod workerMethod)
		{
			var mockedWorker = CreateMockedFsaWorker(workerMethod);
			_fsaWorkerProvider.RegisterWorker(mockedWorker.Object);
		}
	}
}
#endif
// ReSharper restore InconsistentNaming