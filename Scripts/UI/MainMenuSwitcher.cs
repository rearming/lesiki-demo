using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
	public class MainMenuSwitcher : MonoBehaviour
	{
		[SerializeField]
		private float duration = 1f;
		
		[SerializeField]
		[ReadOnly]
		private List<Image> mainMenuImages;

		private List<float> _baseImagesAlpha;

		private void Start()
		{
			_baseImagesAlpha = mainMenuImages.Select(i => i.color.a).ToList();
		}

		public void Switch()
		{
			StartCoroutine(SwitchRoutine());
		}

		private IEnumerator SwitchRoutine()
		{
			for (var i = 0f; i <= duration; i += Time.deltaTime)
			{
				var t = i / duration;
				
				mainMenuImages.ForEachIdx((im, idx) =>
				{
					var newColor = im.color;
					newColor.a = _baseImagesAlpha[idx] * (1 - t);
					im.color = newColor;
				});
				yield return null;
			}
			mainMenuImages.ForEach(im => im.enabled = false);
		}

		[Button]
		private void SetupMainMenuImages()
		{
			mainMenuImages = GetComponentsInChildren<Image>().ToList();
		}
	}
}