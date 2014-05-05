namespace FSA.DTO
{
	/// <summary>
	/// Email summary details
	/// </summary>
	/// <remarks></remarks>
	public class EmailSummaryDetails
	{
		/// <summary>
		/// Gets or sets where the email summary should be sent or not.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the SMTP server.
		/// </summary>
		/// <value>The SMTP server.</value>
		/// <remarks></remarks>
		public string SmtpServer { get; set; }
		
		/// <summary>
		/// Gets or sets from address.
		/// </summary>
		/// <value>From address.</value>
		/// <remarks></remarks>
		public string FromAddress { get; set; }
	}
}