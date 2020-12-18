using System;
using System.Collections;
using Dialog;
using Interfaces;
using ScriptableObjects;
using StatisticsTracking;
using UnityEngine;

namespace Core
{
	public class ProxyQuestionsAsker : MonoBehaviour, IQuestionsAsker
	{
		public event Action<QuestionStatisticInfo> OnAnswer;
		
		private AudioSource _audioSource;

		private EpisodeQuestionsSpec _questionsSpec;

		public void Setup(AudioSource audioSource, QuestionStatisticsInfoPrototype statisticInfoPrototype)
		{
			_audioSource = audioSource;
		}

		/// <summary>
		/// Have to be called as a Coroutine.
		/// </summary>
		/// <param name="questionsSpec"></param>
		/// <returns></returns>
		public IEnumerator AskQuestions(EpisodeQuestionsSpec questionsSpec)
		{
			_questionsSpec = questionsSpec;
			foreach (var questionSpec in _questionsSpec.Questions)
			{
				yield return new WaitForSeconds(_questionsSpec.GeneralQuestionsSpec.DelayBetweenQuestions);
				yield return StartCoroutine(AskQuestion(questionSpec));
			}
		}

		public IEnumerator AskQuestion(QuestionSpec questionSpec)
		{
			Debug.Log($"Asking question [{questionSpec.QuestionElements["BaseQuestion"].Text}]");
			yield return new WaitForSeconds(_questionsSpec.GeneralQuestionsSpec.AnswerListeningDuration);
			Debug.Log($"Correct answers: [{string.Join(", ", questionSpec.CorrectAnswers)}]");
		}
	}
}