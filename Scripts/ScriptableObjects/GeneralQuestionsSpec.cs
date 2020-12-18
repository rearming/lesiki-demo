using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "GeneralQuestionsSpec-1", menuName = "General/Questions Spec", order = 0)]
	public class GeneralQuestionsSpec : ScriptableObject
	{
		[SerializeField]
		[Tooltip("Game will wait for this delay (in seconds) after episode.")]
		private float delayBeforeQuestions = 0.5f;
		public float DelayBeforeQuestions => delayBeforeQuestions;
		
		[SerializeField]
		[Tooltip("Game will wait for this delay (in seconds) between questions.")]
		private float delayBetweenQuestions = 2f;
		public float DelayBetweenQuestions => delayBetweenQuestions;

		[SerializeField]
		[Tooltip("Game will wait for this delay (in seconds) before starting next story.")]
		private float delayAfterQuestions = 1f;
		public float DelayAfterQuestions => delayAfterQuestions;
		
		[SerializeField] 
		private float answerListeningDuration;
		public float AnswerListeningDuration => answerListeningDuration;

		// [SerializeField]
		// private float delayBeforeRepeat = 2f;
		// public float DelayBeforeRepeat => delayBeforeRepeat;
		//
		// [SerializeField]
		// private float delayAfterRepeat = 3f;
		// public float DelayAfterRepeat => delayAfterRepeat;

		[SerializeField]
		private List<QuestionElement> congratsOnCorrectAnswer = new List<QuestionElement>();
		public List<QuestionElement> CongratsOnCorrectAnswer => congratsOnCorrectAnswer;

		public static string DefaultGeneralQuestionsSpec => "DefaultGeneralQuestionsSpec";
	}
}