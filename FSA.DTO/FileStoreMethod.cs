// ReSharper disable InconsistentNaming
namespace FSA.DTO
{
	/// <summary>
	/// File store methods
	/// </summary>
	/// <remarks></remarks>
	public enum FileStoreMethod
	{
		/// <summary>
		/// Direct copy from source to destination.
		/// </summary>
		COPY,

		/// <summary>
		/// Incremental copy from source to destination.
		/// </summary>
		INCREMENTAL
	}
}
// ReSharper restore InconsistentNaming