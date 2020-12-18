using System;
using System.Collections;
using Dialog;
using Interfaces;
using ScriptableObjects;
using StatisticsTracking;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Core
{
	public class QuestionsAsker : MonoBehaviour, IQuestionsAsker
	{
		public event Action<QuestionStatisticInfo> OnAnswer;

		private ISpeechRecognizer _speechRecognizer;
		
		private AudioSource _audioSource;
		private EpisodeQuestionsSpec _questionsSpec;
		
		private QuestionStatisticsInfoPrototype _statisticInfoPrototype;
		private QuestionStatisticInfo _statisticsInfo;

		private void Awake()
		{
			_speechRecognizer = GetComponent<ISpeechRecognizer>();
		}

		public void Setup(AudioSource audioSource, QuestionStatisticsInfoPrototype statisticInfoPrototype)
		{
			_audioSource = audioSource;
			_statisticInfoPrototype = statisticInfoPrototype;
		}

		public IEnumerator AskQuestions(EpisodeQuestionsSpec questionsSpec)
		{
			_questionsSpec = questionsSpec;
			yield return new WaitForSeconds(questionsSpec.GeneralQuestionsSpec.DelayBeforeQuestions);
			for (var i = 0; i < _questionsSpec.Questions.Count; i++)
			{
				var questionSpec = _questionsSpec.Questions[i];
				_statisticsInfo = QuestionStatisticInfo
					.CreateInstance(_statisticInfoPrototype)
					.AddQuestionInfo(i, questionSpec);
				yield return AskQuestion(questionSpec);
				if (i + 1 != _questionsSpec.Questions.Count)
					yield return new WaitForSeconds(_questionsSpec.GeneralQuestionsSpec.DelayBetweenQuestions);
			}
			yield return new WaitForSeconds(questionsSpec.GeneralQuestionsSpec.DelayAfterQuestions);
		}
		
		public IEnumerator AskQuestion(QuestionSpec questionSpec)
		{
			_speechRecognizer.Setup(questionSpec.CorrectAnswers);
			yield return PlayVoice(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.Intro]);
			yield return PlayVoice(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.BaseQuestion]);
			
			yield return _speechRecognizer.Recognize(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.BaseQuestion].Text, 
				_questionsSpec.GeneralQuestionsSpec.AnswerListeningDuration);
			if (_speechRecognizer.IsCorrectAnswer())
			{
				OnAnswer?.Invoke(_statisticsInfo
					.AddTimeBeforeAnswer(_speechRecognizer.TimeBeforeAnswer)
					.AddCorrectAnswerAttempt(1));
				yield return OnCorrectAnswer(questionSpec);
				yield break;
			}
			
			yield return PlayVoice(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.SupportQuestion]);
			
			yield return _speechRecognizer.Recognize(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.BaseQuestion].Text, 
				_questionsSpec.GeneralQuestionsSpec.AnswerListeningDuration);
			if (_speechRecognizer.IsCorrectAnswer())
			{
				
				OnAnswer?.Invoke(_statisticsInfo
					.AddTimeBeforeAnswer(_speechRecognizer.TimeBeforeAnswer)
					.AddCorrectAnswerAttempt(2));
				yield return OnCorrectAnswer(questionSpec);
				yield break;
			}
			
			OnAnswer?.Invoke(_statisticsInfo);
			yield return PlayVoice(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.RepeatOnIncorrect]);
		}

		private IEnumerator PlayVoice(QuestionElement element)
		{
			if (element.Voice == null)
				yield break;
			_audioSource.clip = element.Voice;
			_audioSource.Play();
			yield return new WaitForSeconds(element.Voice.length);
		}

		private IEnumerator OnCorrectAnswer(QuestionSpec questionSpec)
		{
			if (questionSpec.QuestionElements[QuestionSpec.DefaultKeys.Motivator].Voice != null)
				yield return PlayVoice(questionSpec.QuestionElements[QuestionSpec.DefaultKeys.Motivator]);
			else
				yield return PlayVoice(_questionsSpec.GeneralQuestionsSpec.CongratsOnCorrectAnswer.Random(Random.Range));
		}
	}
}
