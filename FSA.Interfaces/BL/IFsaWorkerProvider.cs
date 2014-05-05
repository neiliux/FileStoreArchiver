using FSA.DTO;

namespace FSA.Interfaces.BL
{
	/// <summary>
	/// Provides API for managing FSA workers.
	/// </summary>
	/// <remarks></remarks>
	public interface IFsaWorkerProvider
	{
		/// <summary>
		/// Registers the worker.
		/// </summary>
		/// <param name="worker">The worker.</param>
		/// <remarks></remarks>
		void RegisterWorker(IFsaWorker worker);

		/// <summary>
		/// Gets the worker.
		/// </summary>
		/// <param name="fileStoreDetails">The file store details.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		IFsaWorker GetWorker(FileStoreDetails fileStoreDetails);
	}
}