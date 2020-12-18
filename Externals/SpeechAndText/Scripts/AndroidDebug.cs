using UnityEngine;
using UnityEngine.UI;

namespace Externals.SpeechAndText.Scripts
{
    public class AndroidDebug : MonoBehaviour
    {
        public Text txtLog;
        public Text txtNewLog;

        private void Start()
        {
            SpeechToText.Instance.OnResultCallback += OnResultCallback;
#if UNITY_ANDROID
            SpeechToText.Instance.OnReadyForSpeechCallback += OnReadyForSpeechCallback;
            SpeechToText.Instance.OnEndOfSpeechCallback += OnEndOfSpeechCallback;
            SpeechToText.Instance.OnBeginningOfSpeechCallback += OnBeginningOfSpeechCallback;
            SpeechToText.Instance.OnErrorCallback += OnErrorCallback;
            SpeechToText.Instance.OnPartialResultsCallback += OnPartialResultsCallback;
#else
        gameObject.SetActive(false);
#endif
        }

        private void OnEnable()
        {
            Application.logMessageReceived += AddOnLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= AddOnLog;
        }

        public void AddOnLog(string message, string unityStackTrace, LogType logType)
        {
            if (logType == LogType.Error || logType == LogType.Exception)
                message += '\n' + unityStackTrace;
            txtLog.text += '\n' + message;
            txtNewLog.text = message;
        }

        private void OnResultCallback(string data)
        {
            Debug.Log("Result: " + data);
        }

        private void OnReadyForSpeechCallback(string @params)
        {
            Debug.Log("Ready for the user to start speaking");
        }

        private void OnEndOfSpeechCallback()
        {
            Debug.Log("User stops speaking");
        }

        private void OnBeginningOfSpeechCallback()
        {
            Debug.Log("User has started to speak");
        }

        private void OnErrorCallback(string @params)
        {
            Debug.Log("Error: " + @params);
        }

        private void OnPartialResultsCallback(string @params)
        {
            Debug.Log("Partial recognition results are available " + @params);
        }
    }
}
