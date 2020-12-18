using System.Collections;
using UnityEngine;

namespace UI.Animation
{
	public class InOutSize : MonoBehaviour
	{
		[SerializeField] 
		private AnimationCurve scaleMultiplier;
		
		[SerializeField] 
		private float duration = 0.5f;

		private Vector3 _baseScale;
		private Coroutine _animateCoroutine;

		private void Start()
		{
			_baseScale = transform.localScale;
		}

		public void Animate()
		{
			if (_animateCoroutine != null)
				StopCoroutine(_animateCoroutine);
			_animateCoroutine = StartCoroutine(AnimateRoutine());
		}

		private IEnumerator AnimateRoutine()
		{
			for (var i = 0f; i <= duration; i += Time.deltaTime)
			{
				var t = i / duration;
				transform.localScale = _baseScale * scaleMultiplier.Evaluate(t);
				yield return null;
			}
			transform.localScale = _baseScale;
		}
	}
}