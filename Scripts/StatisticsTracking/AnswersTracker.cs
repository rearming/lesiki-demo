using System;
using System.Collections.Generic;
using Core;
using Interfaces;
using ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils;

namespace StatisticsTracking
{
	public class AnswersTracker : SerializedMonoBehaviour
	{
		[OdinSerialize]
		// ReSharper disable once InconsistentNaming
		private IQuestionsAsker questionsAsker;

		private readonly List<QuestionStatisticInfo> _answerInfos = new List<QuestionStatisticInfo>();

		public AnswersStatistics GetStatistics() => new AnswersStatistics(_answerInfos);
		public event Action OnStatisticsUpdate; 

		private void Start()
		{
			questionsAsker.OnAnswer += OnAnswer;
		}

		private void OnAnswer(QuestionStatisticInfo answerInfo)
		{
			_answerInfos.Add(answerInfo);
			OnStatisticsUpdate?.Invoke();
		}
	}
}