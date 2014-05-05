using System;
using FSA.DTO;

namespace FSA.Interfaces.BL
{
	/// <summary>
	/// Provides API for communication with a FSA worker.
	/// </summary>
	/// <remarks></remarks>
	public interface IFsaWorker
	{
		/// <summary>
		/// Gets the method this worker implements.
		/// </summary>
		/// <remarks></remarks>
		FileStoreMethod Method { get; }

		/// <summary>
		/// Gets the ID if the worker.
		/// </summary>
		/// <remarks></remarks>
		Guid ID { get; }

		/// <summary>
		/// Runs the specified worker run details.
		/// </summary>
		/// <param name="workerRunDetails">The worker run details.</param>
		/// <param name="logger">The logger.</param>
		/// <remarks></remarks>
		void Run(FsaWorkerRunDetails workerRunDetails, IFsaLogger logger);
	}
}