using System.Globalization;
using UnityEngine;

namespace Utils
{
	public class UIUtils : MonoBehaviour
	{
		private Canvas _mainCanvas;
		public Canvas MainCanvas
		{
			get
			{
				if (_mainCanvas == null)
					_mainCanvas = GameObject.FindWithTag("Main Canvas").GetComponent<Canvas>();
				return _mainCanvas;
			}
		}
	}

	public static class LanguageUtils
	{
		public static readonly CultureInfo DefaultCulture = new CultureInfo("ru-RU");
	}
}