using System;
using Dialog;
using ScriptableObjects;
using UnityEngine;

namespace StatisticsTracking
{
	public class QuestionStatisticInfo
	{
		public QuestionStatisticsInfoPrototype Prototype { get; private set; }
		
		public int QuestionNumber { get; private set; }
		public QuestionSpec QuestionSpec { get; private set; }

		/// <summary>
		/// From what attempt was the correct answer given.
		/// -1 means incorrect answer.
		/// </summary>
		public int CorrectAnswerAttempt { get; private set; } = IncorrectAnswer;

		public static readonly int IncorrectAnswer = -1;

		public bool IsCorrectAnswer => CorrectAnswerAttempt != IncorrectAnswer;

		/// <summary>
		/// Time in seconds before player starts to speak.
		/// If the answer wasn't given, will be left with PositiveInfinity.
		/// </summary>
		public float TimeBeforeAnswer { get; private set; } = float.PositiveInfinity;

		public static QuestionStatisticInfo CreateInstance(QuestionStatisticsInfoPrototype prototype)
		{
			return new QuestionStatisticInfo()
			{
				Prototype = prototype
			};
		}
		
		public QuestionStatisticInfo AddQuestionInfo(int questionNumber, QuestionSpec questionSpec)
		{
			QuestionNumber = questionNumber;
			QuestionSpec = questionSpec;
			return this;
		}

		public QuestionStatisticInfo AddTimeBeforeAnswer(float timeBeforeAnswer)
		{
			TimeBeforeAnswer = timeBeforeAnswer;
			return this;
		}

		/// <summary>
		/// Call if answer was correct.
		/// </summary>
		/// <param name="correctAnswerAttempt"></param>
		/// <returns></returns>
		public QuestionStatisticInfo AddCorrectAnswerAttempt(int correctAnswerAttempt)
		{
			CorrectAnswerAttempt = correctAnswerAttempt;
			return this;
		}

		private QuestionStatisticInfo()
		{
		}
	}
}