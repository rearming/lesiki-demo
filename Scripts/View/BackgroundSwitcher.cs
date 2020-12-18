using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
	[Serializable]
	public class BackgroundSwitcher : MonoBehaviour
	{
		[SerializeField]
		private Image current;
		
		[SerializeField]
		private Image currentBlurred;

		[Space]
		
		[SerializeField]
		private Image next;
		
		[SerializeField]
		private Image nextBlurred;

		private EpisodesTranslationSpec _translationSpec;

		private Sprite _nextSprite;
		private Sprite _nextBlurredSprite;
		
		/// <summary>
		/// Have to be called before SwitchBackgrounds() method.
		/// </summary>
		/// <param name="translationSpec"></param>
		/// <param name="nextBackgroundSprite">Episode's background sprite.</param>
		/// <param name="nextBlurredBackgroundSprite">Episode's blurred background sprite.</param>
		public void Setup(EpisodesTranslationSpec translationSpec, Sprite nextBackgroundSprite, Sprite nextBlurredBackgroundSprite)
		{
			_translationSpec = translationSpec;
			_nextSprite = nextBackgroundSprite;
			_nextBlurredSprite = nextBlurredBackgroundSprite;
		}

		/// <summary>
		/// Have to be called as a Coroutine.
		/// </summary>
		/// <returns>Coroutine.</returns>
		/// <exception cref="Exception">Thrown if Setup() method wasn't called first.</exception>
		public IEnumerator SwitchBackgrounds()
		{
			if (_translationSpec == null)
				throw new Exception($"[{nameof(_translationSpec)}] == null. You have to call Setup() method first.");
			
			next.sprite = _nextSprite;
			nextBlurred.sprite = _nextBlurredSprite;
			
			if (current.sprite == next.sprite)
			{
				Debug.LogWarning("current bg sprite == next bg sprite. switch won't be done.");
				yield break;
			}

			for (var rawT = 0f; rawT <= _translationSpec.Duration; rawT += Time.deltaTime)
			{
				var t = rawT / _translationSpec.Duration;
				
				var currentColor = current.color;
				var nextColor = next.color;
				var currentBlurredColor = currentBlurred.color;
				var nextBlurredColor = nextBlurred.color;

				currentColor.a = _translationSpec.Curve.Evaluate(1 - t);
				nextColor.a = _translationSpec.Curve.Evaluate(t);
				currentBlurredColor.a = _translationSpec.Curve.Evaluate(1 - t);
				nextBlurredColor.a = _translationSpec.Curve.Evaluate(t);
				
				current.color = currentColor;
				next.color = nextColor;
				currentBlurred.color = currentBlurredColor;
				nextBlurred.color = nextBlurredColor;
				yield return null;
			}
			
			current.sprite = next.sprite;
			current.color = next.color;
			currentBlurred.sprite = nextBlurred.sprite;
			currentBlurred.color = nextBlurred.color;
			
			next.color = new Color(1f, 1f, 1f, 0f);
			nextBlurred.color = new Color(1f, 1f, 1f, 0f);
		}
	}
}