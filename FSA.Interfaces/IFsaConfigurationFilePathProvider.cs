namespace FSA.Interfaces
{
	/// <summary>
	/// Provides interface for fetching the full path to the configuration xml file.
	/// </summary>
	public interface IFsaConfigurationFilePathProvider
	{
		/// <summary>
		/// Gets the configuration file path.
		/// </summary>
		/// <returns>System.String.</returns>
		string GetConfigurationFilePath(); 
	}
}