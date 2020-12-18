using System.Collections;
using UnityEngine;

namespace UI
{
	public class URLOpener : MonoBehaviour
	{
		[SerializeField]
		private float delay;
		
		public void OpenURL(string url)
		{
			StartCoroutine(OpenURLRoutine(url));
		}

		private IEnumerator OpenURLRoutine(string url)
		{
			yield return new WaitForSeconds(delay);
			Application.OpenURL(url);
		}
	}
}