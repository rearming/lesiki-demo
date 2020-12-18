using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Externals.SpeechAndText.Scripts;
using Interfaces;
using StatisticsTracking;
using UnityEngine;
using UnityEngine.Android;
using Utils;
using Random = UnityEngine.Random;

namespace SpeechRecognition
{
	public class SpeechRecognizer : MonoBehaviour, ISpeechRecognizer
	{
		private List<string> _keywords;

#pragma warning disable 414 // warnings because of compiler definitions (#if !UNITY_EDITOR) 
		private bool _startedSpeeking;
		private bool _recognitionComplete;
#pragma warning restore 414
		private bool _recognitionCorrect;

		private string _result;

		private void Start()
		{
			RequestPermissions();
			
			SpeechToText.Instance.Setting("ru_");
			SpeechToText.Instance.OnResultCallback += OnReceiveResults;
			SpeechToText.Instance.OnPartialResultsCallback += s => _startedSpeeking = true;
		}

		private void RequestPermissions()
		{
#if PLATFORM_ANDROID
			if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
			{
				Permission.RequestUserPermission(Permission.Microphone);
			}
#endif
		}

		public void Setup(List<string> keywords)
		{
			_keywords = keywords;
		}

		public void Reset()
		{
			_startedSpeeking = false;
			_recognitionComplete = false;
			_recognitionCorrect = false;
			_result = "";
		}

		public IEnumerator Recognize(string question, float listeningDuration)
		{
			Reset();
#if PLATFORM_ANDROID && !UNITY_EDITOR
			SpeechToText.Instance.StartRecording(question);
			Debug.Log("Started recording.");
			yield return StartCoroutine(WaitForStartSpeeking(listeningDuration));
			if (_startedSpeeking)
				yield return new WaitWhile(() => !_recognitionComplete);
			SpeechToText.Instance.StopRecording();
			Debug.Log("Stopped recording.");
			CheckRecognitionResults();
#else
			#region Windows Dummy
			
			Debug.Log("[Windows Dummy Speech Recognition]");
			_recognitionComplete = true;

			if (Random.Range(0f, 1f) < 0.4f) // dummy "40% of answers are true"
				_result = string.Join(" ", _keywords);
			else
				_result = "fef";
			TimeBeforeAnswer = Random.Range(0f, listeningDuration);
			
			CheckRecognitionResults();

			yield return null;
			
			#endregion
#endif
		}

		private IEnumerator WaitForStartSpeeking(float listenDuration)
		{
			for (var time = 0f; time < listenDuration; time += Time.deltaTime)
			{
				if (_startedSpeeking)
				{
					TimeBeforeAnswer = time;
					yield break;
				}
				yield return null;
			}
		}

		public bool IsCorrectAnswer()
		{
			return _recognitionCorrect;
		}

		public float TimeBeforeAnswer { get; private set; }

		private void OnReceiveResults(string results)
		{
			Debug.Log($"Receiving results [{results.ToLower(LanguageUtils.DefaultCulture)}].");
			_result = results;
			_recognitionComplete = true;
		}

		private void CheckRecognitionResults()
		{
			foreach (var keyword in _keywords)
			{
				var input = _result.ToLower(LanguageUtils.DefaultCulture);
				var pattern = $".*{keyword.ToLower(LanguageUtils.DefaultCulture)}.*";
				Debug.Log($"Input: [{input}]");
				Debug.Log($"Pattern: [{pattern}]");
				if (Regex.IsMatch(input, pattern))
				{
					_recognitionCorrect = true;
					Debug.Log("Correct answer.");
					return;
				}
			}
		}
	}
}