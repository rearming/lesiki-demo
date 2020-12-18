using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DebugScripts
{
	public class DebugTimer : MonoBehaviour
	{
		private Text _text;

		private Coroutine _timerRoutine;
		
		private void Awake()
		{
			_text = GetComponent<Text>();
		}

		[Button]
		public void StartTimer()
		{
			if (_timerRoutine != null)
				StopCoroutine(_timerRoutine);
			_timerRoutine = StartCoroutine(TimerRoutine());
		}

		private IEnumerator TimerRoutine()
		{
			var timePassed = 0f;
			while (true)
			{
				var timeSpan = TimeSpan.FromSeconds(timePassed);
				_text.text = $"{timeSpan.Minutes}:" +
				             $"{timeSpan.Seconds.ToString().PadLeft(2, '0')}:" +
				             $"{timeSpan.Milliseconds.ToString().PadLeft(3, '0').Substring(0, 2)}";
				timePassed += Time.deltaTime;
				yield return null;
			}
		}
	}
}