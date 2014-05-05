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
		public FsaConfigurationXsdException()
		{
		}

		public FsaConfigurationXsdException(string message)
			: base(message)
		{
		}
	}
}