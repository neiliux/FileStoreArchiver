using System.Diagnostics;
using System.Linq;

namespace FSA.Tests.Shared
{
	/// <summary>
	/// Provides helper methods for manipulating debug/trace listeners.
	/// </summary>
	public class DebugListenerHelpers
	{
		/// <summary>
		/// Registers a listener.
		/// </summary>
		/// <param name="listener">The listener to register.</param>
		/// <param name="removeDefault">if set to <c>true</c> the default listener is removed.</param>
		/// <param name="removeAllListeners">if set to <c>true</c> all registered listener's are removed before the passed listener is registered.</param>
		/// <remarks></remarks>
		public static void RegisterListener(TraceListener listener, bool removeDefault = true, bool removeAllListeners = false)
		{
			if (removeDefault)
			{
				RemoveDefaultListener();
			}

			if (removeAllListeners)
			{
				RemoveAllListeners();
			}

			Trace.Listeners.Add(listener);
		}

		/// <summary>
		/// Removes the default listener.
		/// </summary>
		/// <remarks></remarks>
		private static void RemoveDefaultListener()
		{
			TraceListener listener = Trace.Listeners.OfType<DefaultTraceListener>().FirstOrDefault();
			if (listener != null)
			{
				Trace.Listeners.Remove(listener);
			}
		}

		/// <summary>
		/// Removes all registered listeners.
		/// </summary>
		private static void RemoveAllListeners()
		{
			Trace.Listeners.Clear();
		}
	}
}