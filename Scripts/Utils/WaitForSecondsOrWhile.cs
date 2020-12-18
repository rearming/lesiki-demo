using System;
using UnityEngine;

namespace Utils
{
	
	public class WaitForSecondsOrWhile : CustomYieldInstruction
	{
		private readonly Func<bool> _waitPredicate;
		private float _waitTime;

		public override bool keepWaiting
		{
			get
			{
				_waitTime -= Time.deltaTime;
				return _waitPredicate.Invoke() && _waitTime > 0f;
			}
		}

		/// <summary>
		/// Waits for seconds (scaled time). Exits early if predicate returns false.
		/// </summary>
		/// <param name="waitTime">Scaled wait time.</param>
		/// <param name="waitPredicate">If this predicate returns false, coroutine will be exited early.</param>
		public WaitForSecondsOrWhile(float waitTime, Func<bool> waitPredicate)
		{
			_waitTime = waitTime;
			_waitPredicate = waitPredicate;
		}
	}
}