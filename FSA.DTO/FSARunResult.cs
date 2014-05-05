using System;

namespace FSA.DTO
{
	/// <summary>
	/// Container for results of a FSA run.
	/// </summary>
	/// <remarks></remarks>
	public class FsaRunResult
	{
		/// <summary>
		/// Gets the run exception.
		/// </summary>
		/// <remarks>If no exception this property will be null</remarks>
		public Exception RunException { get; private set; }
		
		/// <summary>
		/// Gets the run time.
		/// </summary>
		/// <remarks></remarks>
		public TimeSpan RunTime { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FsaRunResult"/> class.
		/// </summary>
		/// <param name="runTime">The run time.</param>
		/// <param name="runException">The run exception.</param>
		/// <remarks></remarks>
		public FsaRunResult(TimeSpan runTime, Exception runException)
		{
			RunException = runException;
			RunTime = runTime;
		}
	}
}