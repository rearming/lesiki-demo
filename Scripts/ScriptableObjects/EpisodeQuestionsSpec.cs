using System;
using System.Collections.Generic;
using Dialog;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Episode-1-Questions", menuName = "Story/Episode Questions", order = 0)]
	public class EpisodeQuestionsSpec : SerializedScriptableObject
	{
		[SerializeField]
		[HorizontalGroup("Split", 0.7f)]
		private GeneralQuestionsSpec generalQuestionsSpec;
		public GeneralQuestionsSpec GeneralQuestionsSpec => generalQuestionsSpec;
		
		[Button]
		[HorizontalGroup("Split/right")]
		private void SetupDefaultGeneralSpec()
		{
			generalQuestionsSpec = Resources.Load<GeneralQuestionsSpec>(GeneralQuestionsSpec.DefaultGeneralQuestionsSpec);
		}
		
		[NonSerialized, OdinSerialize]
		// ReSharper disable once InconsistentNaming
		private List<QuestionSpec> questions = new List<QuestionSpec>();
		// ReSharper disable once ConvertToAutoProperty
		public List<QuestionSpec> Questions => questions;
	}
}