using FSA.DTO;

namespace FSA.Interfaces
{
	/// <summary>
	/// Primary interface for FSA
	/// </summary>
	public interface IFileStoreArchiver
	{
		/// <summary>
		/// Runs the fsa.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		FsaRunResult RunFsa();
	}
}