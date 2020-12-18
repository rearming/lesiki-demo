using System;
using ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View
{
	[Serializable]
	public class HighlightSpec
	{
		[SerializeField]
		private float startTime;
		public float StartTime => startTime;

		[SerializeField]
		[OnValueChanged(nameof(CopyRectTransform))]
		private RectTransform transform;

		[SerializeField]
		[ReadOnly]
		private Vector3 pos;
		public Vector3 Pos => pos;

		[SerializeField]
		[ReadOnly]
		private Vector2 size;
		public Vector2 Size => size;
		
		private void CopyRectTransform()
		{
			if (transform == null)
				return;
			pos = transform.anchoredPosition;
			size = transform.sizeDelta;
			transform = null;
		}

		[SerializeField]
		private GameObject prefab;
		public GameObject Prefab => prefab;

		[Header("Animation")]

		[SerializeField]
		private HighlightAnimationSpec appearanceSpec;
		public HighlightAnimationSpec AppearanceSpec => appearanceSpec;

		[SerializeField]
		private HighlightAnimationSpec disappearanceSpec; 
		public HighlightAnimationSpec DisappearanceSpec => disappearanceSpec;

		[Button]
		private void LoadDefaultAnimationSpecs()
		{
			appearanceSpec = Resources.Load<HighlightAnimationSpec>(HighlightAnimationSpec.DefaultAppearanceSpec);
			disappearanceSpec = Resources.Load<HighlightAnimationSpec>(HighlightAnimationSpec.DefaultDisappearanceSpec);
		}
	}
}