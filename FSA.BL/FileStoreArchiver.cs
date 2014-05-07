using System;
using System.Diagnostics;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Interfaces.BL;
using Ninject;

namespace FSA.BL
{
	/// <summary>
	/// FSA entry point.
	/// </summary>
	/// <remarks></remarks>
	public class FileStoreArchiver : IFileStoreArchiver
	{
		private readonly IFsaConfigurationManager _configurationManager;
		private readonly IFsaLogger _fsaLogger;
		private readonly IFsaWorkerProvider _workerProvider;
		private static readonly IFileStoreArchiver InstanceRef;

		/// <summary>
		/// Provides a singleton instance of this class.
		/// </summary>
		/// <value>The instance.</value>
		/// <exception cref="System.NullReferenceException">Instance of IFileStoreArchiver does not exist</exception>
		public static IFileStoreArchiver Instance
		{
			get
			{
				if (InstanceRef == null)
				{
					throw new NullReferenceException("Instance of IFileStoreArchiver does not exist");
				}
				return InstanceRef; 
			}
		}

		/// <summary>
		/// Initializes static members of the <see cref="FileStoreArchiver"/> class.
		/// </summary>
		static FileStoreArchiver()
		{
			// Resolve the instance here in the static CTOR to guarantee
			// thread safety.
			using (IKernel kernel = new StandardKernel(new NinjectMappings()))
			{
				InstanceRef = kernel.Get<IFileStoreArchiver>();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileStoreArchiver"/> class.
		/// </summary>
		/// <param name="configurationManager">The configuration manager.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="workerProvider">The worker provider.</param>
		public FileStoreArchiver(
			IFsaConfigurationManager configurationManager, 
			IFsaLogger logger,
			IFsaWorkerProvider workerProvider)
		{
			_configurationManager = configurationManager;
			_fsaLogger = logger;
			_workerProvider = workerProvider;
		}

		/// <summary>
		/// Invoke the FSA run.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public FsaRunResult RunFsa()
		{
			Debug.Assert(_configurationManager != null);

			Stopwatch runTimer = new Stopwatch();
			FsaRunResult result;
			Exception runException = null;
			try
			{
				runTimer.Start();
				FsaConfiguration configuration = GetConfiguration();
				InvokeFsaFileStoreOperations(configuration);
			}
			catch (Exception ex)
			{
				runException = ex;
			}
			finally
			{
				runTimer.Stop();
				result = new FsaRunResult(runTimer.Elapsed, runException);
			}
			return result;
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		private FsaConfiguration GetConfiguration()
		{
			Debug.Assert(_configurationManager != null);
			var configuration = _configurationManager.GetConfiguration(new FsaConfigurationLoadOptions());
			Debug.Assert(configuration != null);
			return configuration;
		}

		/// <summary>
		/// Invokes the fsa file store operations.
		/// </summary>
		/// <param name="fsaConfiguration">The fsa configuration.</param>
		/// <remarks></remarks>
		private void InvokeFsaFileStoreOperations(FsaConfiguration fsaConfiguration)
		{
			foreach (var fileStore in fsaConfiguration.FileStores)
			{
				IFsaWorker worker = _workerProvider.GetWorker(fileStore);
				worker.Run(new FsaWorkerRunDetails(fsaConfiguration, fileStore), _fsaLogger);
			}
		}
	}
}