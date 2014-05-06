using System;

namespace FSA.BL
{
	/// <summary>
	/// Exception that represents when a method type is not supported.
	/// </summary>
	/// <remarks></remarks>
	[Serializable]
	public class FsaFileStoreMethodNotSupportedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FsaFileStoreMethodNotSupportedException" /> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FsaFileStoreMethodNotSupportedException(string message)
			: base(message)
		{
		}
	}
}