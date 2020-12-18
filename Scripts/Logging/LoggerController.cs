using System;
using System.IO;
using UnityEngine;
using Utils;

namespace Logging
{
	public class LoggerController : MonoBehaviour
	{
		private int _launchCount;

		private string CurrentLogFileName =>
			$"/{Application.productName}_Log_{DateTime.Now.ToShortDateString()}_{_launchCount.ToString()}.txt";

		private string CurrentLogFilePath => Application.persistentDataPath + CurrentLogFileName;
		
		private void Awake()
		{
// #if !UNITY_EDITOR
			SetLaunchCount();
			
			LogUtility.ExtendedLogging = true;
// #endif
		}

		private void Start()
		{
			Application.logMessageReceived += LogUtility.SaveLog;
		}

		private void SetLaunchCount()
		{
			if (PlayerPrefs.HasKey("Launch Count"))
				_launchCount = PlayerPrefs.GetInt("Launch Count") + 1;
			PlayerPrefs.SetInt("Launch Count", _launchCount);
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (hasFocus)
				return;
#if !UNITY_EDITOR
			WriteFullLog();
#endif
		}

		private void WriteFullLog()
		{
			var streamWriter = CreateLogFile();
			streamWriter.Write(LogUtility.LogText);
			Debug.Log($"try to write logger text: [{LogUtility.LogText}]");
			streamWriter.Close();
		}

		private StreamWriter CreateLogFile()
		{
			var filePath = CurrentLogFilePath;
			return File.CreateText(filePath);
		}
	}
}