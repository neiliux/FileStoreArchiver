using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FSA.DTO;
using FSA.Interfaces.BL;

namespace FSA.BL
{
	/// <summary>
	/// Provides management around FSA workers
	/// </summary>
	/// <remarks></remarks>
	public class FsaWorkerProvider : IFsaWorkerProvider
	{
		/// <summary>
		/// List of registered workers.
		/// </summary>
		private readonly List<IFsaWorker> _workers = new List<IFsaWorker>();

		/// <summary>
		/// Registers the worker.
		/// </summary>
		/// <param name="worker">The worker.</param>
		/// <remarks>If an existing worker is already registered with the same method it will be overridden by this registration.</remarks>
		public void RegisterWorker(IFsaWorker worker)
		{
			Debug.Assert(worker != null);
			if (!_workers.Contains(worker))
			{
				// If an existing worker is already registered with the defined method,
				// lets override it.
				var existingWorker = _workers.SingleOrDefault(w => w.Method == worker.Method);
				if (existingWorker != null)
				{
					_workers.Remove(existingWorker);
				}
				_workers.Add(worker);
			}
		}

		/// <summary>
		/// Gets the worker.
		/// </summary>
		/// <param name="fileStoreDetails">The file store details.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public IFsaWorker GetWorker(FileStoreDetails fileStoreDetails)
		{
			return _workers.SingleOrDefault(worker => worker.Method == fileStoreDetails.Method);
		}
	}
}