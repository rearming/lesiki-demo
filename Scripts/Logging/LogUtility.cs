using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Logging
{
	public static class LogUtility
	{
		/// <summary>
		/// Logs all messages with caller method name and filename.
		/// </summary>
		public static bool ExtendedLogging = true;
		
		public static string LogText { get; private set; } = "";
		
		public static void SaveLog(string message, string unityStackTrace, LogType logType)
		{
			if (Application.isEditor)
				return;
			message = ConstructMessage(message, unityStackTrace, logType);
			LogText += message + '\n';
		}
		
		private static string ConstructMessage(string message, string unityStackTrace, LogType logType)
		{
			if (ExtendedLogging)
				return $"[{DateTime.Now.ToString("HH:mm:ss")}] [{Enum.GetName(typeof(LogType), logType)}] " + 
				       $"[{GetFileName(unityStackTrace)}] [{GetMethodName(unityStackTrace)}] -> [{message}]";
			
			return $"[{Enum.GetName(typeof(LogType), logType)}] -> [{message}]";
		}

		private static string GetMethodName(string unityStackTrace)
		{
			var stackString = unityStackTrace.Split('\n')[1];
			
			var methodName = Regex.Match(stackString, @"[:<]([\w]*)[(>]");
			return methodName.Success ? methodName.Groups[1].Value : "";
		}

		private static string GetFileName(string unityStackTrace)
		{
			var stackString = unityStackTrace.Split('\n')[1];

			var fileName = Regex.Match(stackString, @"([\w]+\.cs)");
			return fileName.Success ? fileName.Groups[1].Value : "";
		}
	}
}