using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils;

namespace Dialog
{
	[Serializable]
	public class QuestionSpec
	{
		[NonSerialized, OdinSerialize]
		[DictionaryDrawerSettings(KeyLabel = "Type", ValueLabel = "Question Element")]
		// ReSharper disable once InconsistentNaming
		private Dictionary<string, QuestionElement> questionElements = new Dictionary<string, QuestionElement>();
		public Dictionary<string, QuestionElement> QuestionElements => questionElements;

		[Space]
		[Tooltip("Used as regex.")]
		[SerializeField]
		private List<string> correctAnswers = new List<string>();
		public List<string> CorrectAnswers => correctAnswers;

		public static class DefaultKeys
		{
			public const string Intro = "Intro";
			public const string BaseQuestion = "BaseQuestion";
			public const string SupportQuestion = "SupportQuestion";
			public const string Motivator = "Motivator";
			public const string RepeatOnIncorrect = "RepeatOnIncorrect";
		}

		[Button]
		[PropertyOrder(-1)]
		private void SetupDefaultDictionary()
		{
			questionElements.SafeAdd(DefaultKeys.Intro, new QuestionElement());
			questionElements.SafeAdd(DefaultKeys.BaseQuestion, new QuestionElement());
			questionElements.SafeAdd(DefaultKeys.SupportQuestion, new QuestionElement());
			questionElements.SafeAdd(DefaultKeys.Motivator, new QuestionElement());
			questionElements.SafeAdd(DefaultKeys.RepeatOnIncorrect, new QuestionElement());
		}
	}
}