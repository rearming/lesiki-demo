using System.Collections.Generic;
using System.Linq;

namespace StatisticsTracking
{
	public class AnswersStatistics
	{
		public int TotalAnsweredQuestions { get; private set; }
		
		public int CorrectAnswers { get; private set; }
		public int FirstTryAnswers { get; private set; }
		public int SecondTryAnswers { get; private set; }

		public float AverageAnswerTime { get; private set; }
		
		public AnswersStatistics(List<QuestionStatisticInfo> questionStatisticInfos)
		{
			TotalAnsweredQuestions = questionStatisticInfos.Count;
			
			CorrectAnswers = questionStatisticInfos.Count(info => info.IsCorrectAnswer);
			FirstTryAnswers = questionStatisticInfos.Count(info => info.CorrectAnswerAttempt == 1);
			SecondTryAnswers = questionStatisticInfos.Count(info => info.CorrectAnswerAttempt == 2);

			AverageAnswerTime = questionStatisticInfos
				.Where(info => info.IsCorrectAnswer)
				.Average(info => info.TimeBeforeAnswer);
		}

		private AnswersStatistics()
		{
		}
	}
}