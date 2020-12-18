using System;
using UnityEngine;

namespace Dialog
{
	[Serializable]
	public class QuestionElement
	{
		[TextArea]
		[SerializeField] private string text;
		public string Text => text;

		[SerializeField] private AudioClip voice;
		public AudioClip Voice => voice;
	}
}