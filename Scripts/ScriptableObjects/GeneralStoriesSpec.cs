using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "GeneralStoriesSpec-1", menuName = "General/Stories Spec", order = 0)]
	public class GeneralStoriesSpec : ScriptableObject
	{
		[SerializeField]
		private float delayBeforeStory = 1.5f;

		public float DelayBeforeStory => delayBeforeStory;
		
		public static string DefaultGeneralStoriesSpec => "DefaultGeneralStoriesSpec";
	}
}