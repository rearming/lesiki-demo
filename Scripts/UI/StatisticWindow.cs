using System;
using Sirenix.OdinInspector;
using StatisticsTracking;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
	public class StatisticWindow : MonoBehaviour
	{
		[SerializeField]
		private Text correctAnswers;

		[SerializeField]
		private Text averageAnswerTime;

		private AnswersStatistics _answersStatistics;

		private void Start()
		{
			Singleton<AnswersTracker>.Instance.OnStatisticsUpdate += UpdateWindow;
		}
		
		private void UpdateWindow()
		{
			_answersStatistics = Singleton<AnswersTracker>.Instance.GetStatistics();

			correctAnswers.text = $"{_answersStatistics.CorrectAnswers} / {_answersStatistics.TotalAnsweredQuestions}";
			averageAnswerTime.text = _answersStatistics.AverageAnswerTime.ToString();
		}
	}
}