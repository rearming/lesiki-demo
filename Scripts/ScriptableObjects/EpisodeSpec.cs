using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Episode-1", menuName = "Story/Episode", order = 0)]
	public class EpisodeSpec : SerializedScriptableObject
	{
		[Header("Gameplay")]
		
		[SerializeField]
		private string episodeName;
		public string EpisodeName => episodeName;
		
		[SerializeField]
		private AudioClip readerVoice;
		public AudioClip ReaderVoice => readerVoice;

		[SerializeField]
		private EpisodeQuestionsSpec episodeQuestions;
		public EpisodeQuestionsSpec EpisodeQuestions => episodeQuestions;

		[Header("View")]

		[SerializeField]
		private Sprite background;
		public Sprite Background => background;

		[SerializeField]
		private Sprite blurredBackground;
		public Sprite BlurredBackground => blurredBackground;
		
		[NonSerialized]
		[OdinSerialize]
		[DictionaryDrawerSettings(KeyLabel = "Character", ValueLabel = "Highlight Settings")]
		private Dictionary<string, HighlightSpec> highlights = new Dictionary<string, HighlightSpec>();
		public Dictionary<string, HighlightSpec> Highlights => highlights;
	}
}