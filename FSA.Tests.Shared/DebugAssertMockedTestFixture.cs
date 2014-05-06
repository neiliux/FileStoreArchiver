namespace FSA.Tests.Shared
{
	/// <summary>
	/// Base class for test fixtures that mocks Debug.Assert calls so they can be caught by unit tests.
	/// </summary>
	public abstract class DebugAssertMockedTestFixture
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DebugAssertMockedTestFixture"/> class.
		/// </summary>
		protected DebugAssertMockedTestFixture()
		{
			DebugListenerHelpers.RegisterListener(FailTestOnDebugAssetFailure.GetInstance());
		}
	}
}