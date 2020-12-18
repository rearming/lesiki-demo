using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Highlight Animations Spec", menuName = "View/Highlight Animations Spec", order = 0)]
	public class HighlightAnimationSpec : ScriptableObject
	{
		[SerializeField]
		[Tooltip("Controls alpha on the highlight when it is appearing/disappearing. " +
		         "Alpha will be multiplied with curve's value.")]
		private AnimationCurve alphaCurve;
		public AnimationCurve AlphaCurve => alphaCurve;

		[SerializeField]
		[Min(0.1f)]
		private float duration;
		public float Duration => duration;

		public static string DefaultAppearanceSpec = "DefaultHighlightAppearanceSpec";
		public static string DefaultDisappearanceSpec = "DefaultHighlightDisappearanceSpec";
	}
}