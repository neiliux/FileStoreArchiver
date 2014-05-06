using System;
using System.Diagnostics;
using FSA.DTO;
using FSA.Interfaces;
using FSA.Interfaces.BL;

namespace FSA.BL
{
	/// <summary>
	/// FSA entry point.
	/// </summary>
	/// <remarks></remarks>
	public class FileStoreArchiver : IFileStoreArchiver
	{
		/// <summary>
		/// FSA Configuration manager.
		/// </summary>
		private readonly IFsaConfigurationManager _configurationManager;

		/// <summary>
		/// Full path to configuration Xml file.
		/// </summary>
		private readonly string _pathToConfigXml;

		/// <summary>
		/// Logger.
		/// </summary>
		private readonly IFsaLogger _fsaLogger;

		/// <summary>
		/// Provides management for workers.
		/// </summary>
		private readonly IFsaWorkerProvider _workerProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileStoreArchiver"/> class.
		/// </summary>
		/// <param name="pathToConfigXml">The path to config XML.</param>
		/// <remarks>This CTOR is for release builds.</remarks>
		public FileStoreArchiver(string pathToConfigXml)
		{
			_pathToConfigXml = pathToConfigXml;
			_configurationManager = new FsaConfigurationManager();
			_fsaLogger = new FsaLogger();
			_workerProvider = new FsaWorkerProvider();			
		}

#if DEBUG
		/// <summary>
		/// Initializes a new instance of the <see cref="FileStoreArchiver"/> class.
		/// </summary>
		/// <param name="pathToConfigXml">The path to config XML.</param>
		/// <param name="configurationManager">The configuration manager.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="workerProvider">The worker provider.</param>
		/// <remarks>This CTOR is for debug builds.</remarks>
		public FileStoreArchiver(string pathToConfigXml,
								IFsaConfigurationManager configurationManager, 
								IFsaLogger logger,
								IFsaWorkerProvider workerProvider)
		{
			_configurationManager = configurationManager;
			_pathToConfigXml = pathToConfigXml;
			_fsaLogger = logger;
			_workerProvider = workerProvider;
		}
#endif

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
			var configuration = _configurationManager.GetConfiguration(new FsaConfigurationLoadOptions { PathToConfigXmlFile = _pathToConfigXml });
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