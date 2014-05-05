namespace FSA.Tests.Shared
{
	/// <summary>
	/// Base class for test fixtures that mocks Debug.Assert calls so they can be caught by unit tests.
	/// </summary>
	public abstract class DebugAssertMockedTestClass
	{
		protected DebugAssertMockedTestClass()
		{
			DebugListenerHelpers.RegisterListener(FailTestOnDebugAssetFailure.GetInstance());
		}
	}
}