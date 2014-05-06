namespace FSA.DTO
{
	/// <summary>
	/// Provides details to a FSA worker
	/// </summary>
	/// <remarks></remarks>
	public class FsaWorkerRunDetails
	{
		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <remarks></remarks>
		public FsaConfiguration Configuration { get; private set; }

		/// <summary>
		/// Gets the file store details.
		/// </summary>
		/// <remarks></remarks>
		public FileStoreDetails FileStoreDetails { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaWorkerRunDetails"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="fileStoreDetails">The file store details.</param>
		/// <remarks></remarks>
		public FsaWorkerRunDetails(
			FsaConfiguration configuration,
			FileStoreDetails fileStoreDetails)
		{
			Configuration = configuration;
			FileStoreDetails = fileStoreDetails;
		}
	}
}