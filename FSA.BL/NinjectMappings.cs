using FSA.DAL;
using FSA.Interfaces;
using FSA.Interfaces.BL;
using FSA.Interfaces.DAL;
using Ninject.Modules;

namespace FSA.BL
{
	/// <summary>
	/// Ninject mapping module for FSA.
	/// </summary>
	public class NinjectMappings : NinjectModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			Bind<IFsaConfigurationFilePathProvider>().To<FsaConfigurationFilePathProvider>();
			Bind<IFsaConfigurationDataProvider>().To<FsaConfigurationDataProvider>();
			Bind<IFsaConfigurationValidator>().To<FsaConfigurationValidator>();
			Bind<IFsaConfigurationManager>().To<FsaConfigurationManager>();

			Bind<IFsaConfigurationDal>().To<FsaConfigurationDal>();

			Bind<IFsaLogger>().To<FsaLogger>();

			Bind<IFsaWorkerProvider>().To<FsaWorkerProvider>();
			Bind<IFileStoreArchiver>().To<FileStoreArchiver>();
		}
	}
}