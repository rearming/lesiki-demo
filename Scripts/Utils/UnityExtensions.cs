using UnityEngine;

namespace Utils
{
	public static class UnityExtensions
	{
		public static void PlayScaled(this AudioSource audioSource)
		{
			audioSource.pitch = Time.timeScale;
			audioSource.Play();
		}
	}
}