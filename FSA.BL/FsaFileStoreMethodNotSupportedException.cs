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
		public FsaFileStoreMethodNotSupportedException(string message)
			: base(message)
		{
		}
	}
}