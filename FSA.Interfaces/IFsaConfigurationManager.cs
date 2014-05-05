using FSA.DTO;

namespace FSA.Interfaces
{
	/// <summary>
	/// Provides API for FSA configuration
	/// </summary>
	public interface IFsaConfigurationManager
	{
		/// <summary>
		/// Retrieves the FSA configuration
		/// </summary>
		/// <returns></returns>
		FsaConfiguration GetConfiguration(FsaConfigurationLoadOptions options);
	}
}