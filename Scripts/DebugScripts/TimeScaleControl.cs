using UnityEngine;

namespace DebugScripts
{
	public class TimeScaleControl : MonoBehaviour
	{
		[SerializeField]
		private AudioSource audioSource;
		
		public void ChangeTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}

		public void ChangeAudioScale(float timeScale)
		{
			audioSource.pitch = timeScale;
		}
	}
}