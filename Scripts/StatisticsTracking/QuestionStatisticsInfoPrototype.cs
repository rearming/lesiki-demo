using Dialog;
using ScriptableObjects;

namespace StatisticsTracking
{
	public class QuestionStatisticsInfoPrototype
	{
		public int StoryNumber { get; private set; }
		public StorySpec StorySpec { get; private set; }
		public int EpisodeNumber { get; private set; }
		public EpisodeSpec EpisodeSpec { get; private set; }
		
		public QuestionStatisticsInfoPrototype(int storyNumber, StorySpec storySpec, int episodeNumber, EpisodeSpec episodeSpec)
		{
			StoryNumber = storyNumber;
			StorySpec = storySpec;
			EpisodeNumber = episodeNumber;
			EpisodeSpec = episodeSpec;
		}

		private QuestionStatisticsInfoPrototype()
		{
		}
	}
}