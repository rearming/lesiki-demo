using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Story-1", menuName = "Story/Story", order = 0)]
	public class StorySpec : SerializedScriptableObject
	{
		[Header("Gameplay")]

		[SerializeField]
		private string storyName;
		public string StoryName => storyName;

		[SerializeField]
		[HorizontalGroup("Split", 0.7f)]
		private GeneralStoriesSpec generalStoriesSpec;
		public GeneralStoriesSpec GeneralStoriesSpec => generalStoriesSpec;
		
		[Button]
		[HorizontalGroup("Split/right")]
		private void SetupDefaultGeneralSpec()
		{
			generalStoriesSpec = Resources.Load<GeneralStoriesSpec>(GeneralStoriesSpec.DefaultGeneralStoriesSpec);
		}
		
		[OdinSerialize]
		[ReadOnly]
		[Tooltip("Episodes with their names.")]
		private Dictionary<string, EpisodeSpec> episodes = new Dictionary<string, EpisodeSpec>();

		[SerializeField]
		[OnCollectionChanged(nameof(SetupEpisodes))]
		[Tooltip("You can add new episodes here.")]
		private List<EpisodeSpec> rawEpisodes = new List<EpisodeSpec>();

		[Header("View")]

		[SerializeField]
		private EpisodesTranslationSpec translationBetweenEpisodes;
		public EpisodesTranslationSpec TranslationBetweenEpisodes => translationBetweenEpisodes;
			
		public EpisodeSpec GetEpisodeSpec(int episodeNum) => rawEpisodes[episodeNum];

		public int GetEpisodeNumber(EpisodeSpec episodeSpec) => rawEpisodes.IndexOf(episodeSpec);

		public EpisodeSpec GetEpisodeSpec(string episodeName) => episodes[episodeName];

		public int EpisodesCount => episodes.Count;
		
		private void SetupEpisodes() => episodes = rawEpisodes.ToDictionary(e => e.EpisodeName);
	}
}