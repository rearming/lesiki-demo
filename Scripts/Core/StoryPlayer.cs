using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using StatisticsTracking;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Core
{
	public class StoryPlayer : SerializedMonoBehaviour
	{
		[Header("Settings")]
		
		[SerializeField]
		[OnCollectionChanged(nameof(SetupStoriesDict))]
		// ReSharper disable once InconsistentNaming
		public List<StorySpec> stories = new List<StorySpec>();
		
		[OdinSerialize]
		[ReadOnly]
		private Dictionary<string, StorySpec> _storiesDictionary;

		[Header("Scene References")]

		[SerializeField]
		private AudioSource audioSource;

		private StorySpec _currentStory;
		private EpisodeSpec _currentEpisode;

		private BackgroundSwitcher _backgroundSwitcher;
		private EpisodePlayer _episodePlayer;
		private HighlightController _highlightController;
		private IQuestionsAsker _questionsAsker;
		
		[Header("Debug")]
		
		[SerializeField]
		private UnityEvent onStartPlayingEpisode;
		
		private void Awake()
		{
			_backgroundSwitcher = GetComponent<BackgroundSwitcher>();
			_episodePlayer = GetComponent<EpisodePlayer>();
			_highlightController = GetComponent<HighlightController>();
			_questionsAsker = GetComponent<IQuestionsAsker>();
		}

		[Button]
		public void PlayFirstStoryDebug()
		{
			PlayStory(_storiesDictionary.Keys.First());
		}
		
		[Button]
		public void PlayStory(string storyName)
		{
			if (!_storiesDictionary.TryGetValue(storyName, out _currentStory))
				throw new Exception($"Can't find [{storyName}] story!");
			StartCoroutine(PlayStoryRoutine());
		}
		
		[Button]
		public void PlayStory(int storyNum)
		{
			_currentStory = stories[storyNum];
			StartCoroutine(PlayStoryRoutine());
		}

		private IEnumerator PlayStoryRoutine()
		{
			yield return new WaitForSeconds(_currentStory.GeneralStoriesSpec.DelayBeforeStory);
			Debug.Log($"Playing story [{_currentStory.StoryName}]");
			for (var i = 0; i < _currentStory.EpisodesCount; i++)
			{
				_currentEpisode = _currentStory.GetEpisodeSpec(i);
				Debug.Log($"Starting episode [{i.ToString()}] - [{_currentEpisode.EpisodeName}]");

				_backgroundSwitcher.Setup(_currentStory.TranslationBetweenEpisodes, _currentEpisode.Background, _currentEpisode.BlurredBackground);
				_episodePlayer.Setup(_currentEpisode, audioSource);
				_highlightController.Setup(_currentEpisode.Highlights.Values);
				_questionsAsker.Setup(audioSource, GetStatisticsInfoPrototype());

				yield return StartCoroutine(_backgroundSwitcher.SwitchBackgrounds());
				onStartPlayingEpisode?.Invoke();
				_highlightController.EnableHighlights();
				yield return StartCoroutine(_episodePlayer.Play());
				yield return StartCoroutine(_questionsAsker.AskQuestions(_currentEpisode.EpisodeQuestions));
			}
			Debug.Log("There are no episodes left, story has ended.");
		}

		private QuestionStatisticsInfoPrototype GetStatisticsInfoPrototype()
		{
			return new QuestionStatisticsInfoPrototype(
				stories.IndexOf(_currentStory),
				_currentStory,
				_currentStory.GetEpisodeNumber(_currentEpisode),
				_currentEpisode
			);
		}
		
		[Button]
		private void SetupStoriesDict() => _storiesDictionary = stories.ToDictionary(s => s.StoryName);
	}
}