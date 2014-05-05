namespace FSA.DTO
{
	/// <summary>
	/// Represents an attribute in a FSA configuration block.
	/// </summary>
	/// <remarks></remarks>
	public class FsaConfigurationAttribute
	{
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		/// <remarks></remarks>
		public string Key { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		/// <remarks></remarks>
		public string Value { get; set; }
	}
}