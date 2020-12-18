using System;
using System.Collections;
using Dialog;
using ScriptableObjects;
using StatisticsTracking;
using UnityEngine;

namespace Interfaces
{
	public interface IQuestionsAsker
	{
		void Setup(AudioSource audioSource, QuestionStatisticsInfoPrototype statisticInfoPrototype);
		IEnumerator AskQuestions(EpisodeQuestionsSpec questionsSpec);
		IEnumerator AskQuestion(QuestionSpec questionSpec);

		event Action<QuestionStatisticInfo> OnAnswer;
	}
}