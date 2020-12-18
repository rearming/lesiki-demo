using System.Collections;
using System.Collections.Generic;
using StatisticsTracking;

namespace Interfaces
{
	public interface ISpeechRecognizer
	{
		void Setup(List<string> correctAnswers);
		IEnumerator Recognize(string question, float listeningDuration);
		bool IsCorrectAnswer();
		float TimeBeforeAnswer { get; }
	}
}