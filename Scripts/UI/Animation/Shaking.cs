using System.Collections;
using UnityEngine;

namespace UI.Animation
{
	public class Shaking : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Duration of one shake")]
		private float shakeDuration = 0.5f;
		
		[SerializeField]
		[Min(0)]
		private int shakesCount = 1;

		[SerializeField]
		private float shakeAngle = 10;

		private float _baseRotation;

		private void Start()
		{
			_baseRotation = transform.localEulerAngles.z;
		}

		public void Animate()
		{
			StartCoroutine(AnimateRoutine());
		}

		private IEnumerator AnimateRoutine()
		{
			for (var i = 0; i < shakesCount; i++)
			{
				yield return StartCoroutine(Shake());
			}
			transform.localEulerAngles = new Vector3(0, 0, _baseRotation);
		}

		private IEnumerator Shake()
		{
			yield return StartCoroutine(Rotate(_baseRotation, _baseRotation + shakeAngle, shakeDuration / 4));
			yield return StartCoroutine(Rotate(_baseRotation + shakeAngle, _baseRotation, shakeDuration / 4));
			yield return StartCoroutine(Rotate(_baseRotation, _baseRotation - shakeAngle, shakeDuration / 4));
			yield return StartCoroutine(Rotate(_baseRotation - shakeAngle, _baseRotation, shakeDuration / 4));
		}

		private IEnumerator Rotate(float rotationA, float rotationB, float duration)
		{
			for (var i = 0f; i <= duration; i += Time.deltaTime)
			{
				var t = i / duration;
				transform.localEulerAngles = new Vector3(0, 0, 
					Mathf.LerpAngle(rotationA, rotationB, t));
				yield return null;
			}
		}
	}
}