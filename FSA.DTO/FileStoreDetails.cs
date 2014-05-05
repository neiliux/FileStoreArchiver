namespace FSA.DTO
{
	/// <summary>
	/// File store details
	/// </summary>
	/// <remarks></remarks>
	public class FileStoreDetails
	{
		/// <summary>
		/// Gets or sets the method.
		/// </summary>
		public FileStoreMethod Method { get; set; }
		
		/// <summary>
		/// Gets or sets the source directory.
		/// </summary>
		public string SourceDirectory { get; set; }

		/// <summary>
		/// Gets or sets the destination directory.
		/// </summary>
		public string DestinationDirectory { get; set; }

		/// <summary>
		/// Gets or sets the configuration section.
		/// </summary>
		/// <value>The configuration section.</value>
		/// <remarks></remarks>
		public FsaConfigurationSection ConfigurationSection { get; set; }
	}
}