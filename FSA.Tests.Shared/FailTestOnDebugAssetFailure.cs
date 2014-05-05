using System;
using System.Diagnostics;

namespace FSA.Tests.Shared
{
	/// <summary>
	/// Provides a debug/trace listener that will invoke a unit test failure
	/// when a debug/trace assert fails.
	/// </summary>
	public class FailTestOnDebugAssetFailure : TraceListener
	{
		/// <summary>
		/// Determines if a debug/trace fail will cause the unit test to fail.
		/// </summary>
		[ThreadStatic]
		private static bool _disable;

		/// <summary>
		/// Instance of listener.
		/// </summary>
		private static FailTestOnDebugAssetFailure _instance;

		/// <summary>
		/// Lock object for concurrent requests.
		/// </summary>
		private static readonly object Lock = new object();

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public static FailTestOnDebugAssetFailure GetInstance()
		{
			if (_instance != null)
			{
				return _instance;
			}

			lock (Lock)
			{
				if (_instance == null)
				{
					_instance = new FailTestOnDebugAssetFailure();
				}
			}
			return _instance;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FailTestOnDebugAssetFailure"/> is disabled.
		/// </summary>
		/// <value><c>true</c> if disable; otherwise, <c>false</c>.</value>
		/// <remarks></remarks>
		public static bool Disable
		{
			get
			{
				return _disable;
			}
			set
			{
				_disable = value;
			}
		}

		/// <summary>
		/// Emits an error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener"/> class.
		/// </summary>
		/// <param name="message">A message to emit.</param>
		/// <remarks></remarks>
		public override void Fail(string message)
		{
			if (!Disable)
			{
				NUnit.Framework.Assert.Fail(message);
			}
		}

		/// <summary>
		/// Emits an error message and a detailed error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener"/> class.
		/// </summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		/// <remarks></remarks>
		public override void Fail(string message, string detailMessage)
		{
			if (!Disable)
			{
				NUnit.Framework.Assert.Fail("{0}\n{1}", message, detailMessage);
			}
		}

		/// <summary>
		/// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
		/// </summary>
		/// <param name="message">A message to write.</param>
		/// <remarks></remarks>
		public override void Write(string message)
		{
		}

		/// <summary>
		/// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
		/// </summary>
		/// <param name="message">A message to write.</param>
		/// <remarks></remarks>
		public override void WriteLine(string message)
		{
		}
	}
}