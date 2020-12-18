using System;
using UnityEngine;

namespace Externals.SpeechAndText.Scripts
{
    public class SpeechToText : MonoBehaviour
    {
	    #region Init

	    public static SpeechToText Instance { get; private set; }

	    private void Awake()
        {
	        if (Instance != null)
	        {
		        DestroyImmediate(this);
		        return;
	        }
            Instance = this;
        }
        #endregion

        public event Action<string> OnResultCallback;

        public void Setting(string language)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        _TAG_SettingSpeech(language);
#elif UNITY_ANDROID
        AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
        javaUnityClass.CallStatic("SettingSpeechToText", language);
#endif
        }
        public void StartRecording(string message = "")
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        _TAG_startRecording();
#elif UNITY_ANDROID
        if (isShowPopupAndroid)
        {
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
            javaUnityClass.CallStatic("OpenSpeechToText", message);
        }
        else
        {
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
            javaUnityClass.CallStatic("StartRecording");
        }
#endif
        }
        
        public void StopRecording()
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        _TAG_stopRecording();
#elif UNITY_ANDROID
        if (isShowPopupAndroid == false)
        {
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
            javaUnityClass.CallStatic("StopRecording");
        }
#endif
        }

#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void _TAG_startRecording();

        [DllImport("__Internal")]
        private static extern void _TAG_stopRecording();

        [DllImport("__Internal")]
        private static extern void _TAG_SettingSpeech(string _language);
#endif

        public void onMessage(string message)
        {
        }
        
        public void onErrorMessage(string message)
        {
            Debug.Log($"Error in textToSpeech: [{message}]");
        }
        
        /// <summary>
		///  Called when recognition results are ready.
        /// </summary>
        public void onResults(string results)
        {
	        OnResultCallback?.Invoke(results);
        }
        
        #region Android STT custom
#if UNITY_ANDROID
        #region Error Code
        /// <summary>
		/// Network operation timed out. 
		/// </summary>
        public const int ErrorNetworkTimeout = 1;
        /// <summary>
		/// Other network related errors. 
		/// </summary>
        public const int ErrorNetwork = 2;
        /// <summary>
		/// Audio recording error. 
		/// </summary>
        public const int ErrorAudio = 3;
        /// <summary>
		/// Server sends error status. 
		/// </summary>
        public const int ErrorServer = 4;
        /// <summary>
		/// Other client side errors. 
		/// </summary>
        public const int ErrorClient = 5;
        /// <summary>
		/// No speech input 
		/// </summary>
        public const int ErrorSpeechTimeout = 6;
        /// <summary>
		/// No recognition result matched. 
		/// </summary>
        public const int ErrorNoMatch = 7;
        /// <summary>
		/// RecognitionService busy. 
		/// </summary>
        public const int ErrorRecognizerBusy = 8;
        /// <summary>
		/// Insufficient permissions 
		/// </summary>
        public const int ErrorInsufficientPermissions = 9;

        private static string GetErrorText(int errorCode)
        {
            string message;
            switch (errorCode)
            {
	            case ErrorAudio:
		            message = "Audio recording error";
		            break;
	            case ErrorClient:
		            message = "Client side error";
		            break;
	            case ErrorInsufficientPermissions:
		            message = "Insufficient permissions";
		            break;
	            case ErrorNetwork:
		            message = "Network error";
		            break;
	            case ErrorNetworkTimeout:
		            message = "Network timeout";
		            break;
	            case ErrorNoMatch:
		            message = "No match";
		            break;
	            case ErrorRecognizerBusy:
		            message = "RecognitionService busy";
		            break;
	            case ErrorServer:
		            message = "error from server";
		            break;
	            case ErrorSpeechTimeout:
		            message = "No speech input";
		            break;
	            default:
		            message = "Didn't understand, please try again.";
		            break;
            }

            return message;
        }
        #endregion
        
        /// <summary>
        /// Callbacks will work like shit if this setting is toggled.
        /// </summary>
        [Tooltip("Callbacks will work like shit if this setting is toggled.")]
        public bool isShowPopupAndroid = true;

        public void TogglePopup(bool toggle) => isShowPopupAndroid = toggle;
        
        /// <summary>
        /// Called when the endpointer is ready for the user to start speaking.
        /// </summary>
        public event Action<string> OnReadyForSpeechCallback;
        
        /// <summary>
        /// Called after the user stops speaking.
        /// </summary>
        public event Action OnEndOfSpeechCallback;

        /// <summary>
        /// The user has started to speak.
        /// Actually called when system started to listen, independent of user speech.
        /// </summary>
        public event Action OnBeginningOfSpeechCallback;
        
        /// <summary>
        /// A network or recognition error occurred.
        /// </summary>
        public event Action<string> OnErrorCallback;
        
        /// <summary>
        /// Called when partial recognition results are available.
        /// </summary>
        public event Action<string> OnPartialResultsCallback;
        
        public void onReadyForSpeech(string @params)
        {
	        OnReadyForSpeechCallback?.Invoke(@params);
        }
        
        public void onEndOfSpeech(string paramsNull)
        {
	        OnEndOfSpeechCallback?.Invoke();
        }
        
        public void onRmsChanged(string value)
        {
        }
        
        public void onBeginningOfSpeech(string paramsNull)
        {
	        OnBeginningOfSpeechCallback?.Invoke();
        }
        
        public void onError(string value)
        {
            var error = int.Parse(value);
            var message = GetErrorText(error);
            Debug.Log(message);

            OnErrorCallback?.Invoke(message);
        }
        
        public void onPartialResults(string @params)
        {
	        OnPartialResultsCallback?.Invoke(@params);
        }

#endif
        #endregion
    }
}