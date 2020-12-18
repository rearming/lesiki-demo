using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using View;

namespace Core
{
	public class HighlightController : MonoBehaviour
	{
		private IEnumerable<HighlightSpec> _highlightSpec;

		private static readonly Vector2 DefaultResolution = new Vector2(1920, 1080);

		private Coroutine _higlightsCoroutine;
		
		[Button]
		public void DebugHighlight(List<HighlightSpec> specs)
		{
			_highlightSpec = specs;
			EnableHighlights();
		}
		
		public void Setup(IEnumerable<HighlightSpec> highlightSpec)
		{
			_highlightSpec = highlightSpec;
		}

		public void EnableHighlights()
		{
			if (_higlightsCoroutine != null)
			{
				Debug.LogWarning("Highlight was enabled, stopping coroutine.");
				StopCoroutine(_higlightsCoroutine);
			}
			Debug.Log("Starting highlight coroutine.");
			_higlightsCoroutine = StartCoroutine(EnableHighlightsRoutine());
		}

		private IEnumerator EnableHighlightsRoutine()
		{
			Debug.Log("Highlight coroutine started.");
			var timePassed = 0f;
			
			foreach (var highlightSpec in _highlightSpec)
			{
				yield return new WaitForSeconds(highlightSpec.StartTime - timePassed);
				timePassed += highlightSpec.StartTime + highlightSpec.AppearanceSpec.Duration +
				              highlightSpec.DisappearanceSpec.Duration;
				
				var highlightGO = Instantiate(highlightSpec.Prefab, Singleton<UIUtils>.Instance.MainCanvas.transform);
				var highlightRect = highlightGO.GetComponent<RectTransform>();
				var highlightImage = highlightGO.GetComponent<Image>();

				(highlightRect.localScale, highlightRect.sizeDelta, highlightRect.anchoredPosition) = 
					CorrectRect(highlightSpec.Size, highlightSpec.Pos);

				var baseHighlightColor = highlightImage.color;
				
				yield return StartCoroutine(HighlightControlAlpha(highlightImage, baseHighlightColor,
					highlightSpec.AppearanceSpec.AlphaCurve, highlightSpec.AppearanceSpec.Duration));
				yield return StartCoroutine(HighlightControlAlpha(highlightImage, baseHighlightColor,
					highlightSpec.DisappearanceSpec.AlphaCurve, highlightSpec.DisappearanceSpec.Duration));
				
				Destroy(highlightGO);
			}
		}

		private (Vector3, Vector2, Vector3) CorrectRect(Vector2 size, Vector3 pos)
		{
			var currentResolution = new Vector2(Screen.width, Screen.height);
			var coeff = currentResolution / DefaultResolution;
			
			pos *= coeff.y;
			// pos *= coeff;
			return (Vector3.one * coeff.y, size, pos);
		}
		
		private IEnumerator HighlightControlAlpha(Image highlight, Color baseHighlightColor, AnimationCurve curve, float duration)
		{
			for (var rawT = 0f; rawT <= duration; rawT += Time.deltaTime)
			{
				var t = rawT / duration;
				var color = baseHighlightColor;
				color.a *= curve.Evaluate(t);
				highlight.color = color;
				yield return null;
			}
		}
	}
}