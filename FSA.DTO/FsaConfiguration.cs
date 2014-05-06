using System.Collections.Generic;

namespace FSA.DTO
{
	/// <summary>
	/// FSA Configuration
	/// </summary>
	public class FsaConfiguration
	{
		/// <summary>
		/// Gets the email summary details.
		/// </summary>
		/// <remarks></remarks>
		public EmailSummaryDetails EmailSummaryDetails { get; private set; }

		/// <summary>
		/// Gets the file stores.
		/// </summary>
		/// <remarks></remarks>
		public IEnumerable<FileStoreDetails> FileStores { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfiguration"/> class.
		/// </summary>
		/// <param name="emailSummaryDetails">The email summary details.</param>
		/// <param name="fileStores">The file stores.</param>
		/// <remarks></remarks>
		public FsaConfiguration(
			EmailSummaryDetails emailSummaryDetails,
			IEnumerable<FileStoreDetails> fileStores)
		{
			EmailSummaryDetails = emailSummaryDetails;
			FileStores = fileStores;
		}
	}
}