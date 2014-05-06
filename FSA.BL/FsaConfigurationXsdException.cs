using System;

namespace FSA.BL
{
	/// <summary>
	/// Exception that represents a XSD based configuration validation error.
	/// </summary>
	/// <remarks></remarks>
	[Serializable]
	public class FsaConfigurationXsdException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationXsdException"/> class.
		/// </summary>
		public FsaConfigurationXsdException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaConfigurationXsdException" /> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FsaConfigurationXsdException(string message)
			: base(message)
		{
		}
	}
}