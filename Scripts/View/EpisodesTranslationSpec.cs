using System;
using UnityEngine;

namespace View
{
	[Serializable]
	public class EpisodesTranslationSpec
	{
		[SerializeField]
		private AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		public AnimationCurve Curve => curve;

		[SerializeField]
		[Range(0.1f, 5f)]
		private float duration = 1f;
		public float Duration => duration;
	}
}