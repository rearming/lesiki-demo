using System;
using System.Collections;
using ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Core
{
	public class EpisodePlayer : MonoBehaviour
	{
		private EpisodeSpec _episodeSpec;
		
		private AudioSource _audioSource;

		private bool _stopPlaying;

		public void Setup(EpisodeSpec episodeSpec, AudioSource audioSource)
		{
			_episodeSpec = episodeSpec;
			_audioSource = audioSource;
		}

		/// <summary>
		/// Have to be called as a Coroutine.
		/// </summary>
		/// <returns>Coroutine.</returns>
		public IEnumerator Play()
		{
			if (_episodeSpec == null)
				throw new Exception($"[{nameof(_episodeSpec)}] == null. You have to call Setup() method first.");

			_stopPlaying = false;
			yield return StartCoroutine(PlayVoice());
		}

		private IEnumerator PlayVoice()
		{
			_audioSource.clip = _episodeSpec.ReaderVoice;
			_audioSource.Play();
			Debug.Log($"Started playing voice of [{_episodeSpec.EpisodeName}]");
			yield return new WaitForSecondsOrWhile(_episodeSpec.ReaderVoice.length, () => !_stopPlaying);
			Debug.Log($"Stopped playing voice of [{_episodeSpec.EpisodeName}]");
		}

		[Button]
		public void SkipEpisode()
		{
			_audioSource.Stop();
			_stopPlaying = true;
		}
	}
}