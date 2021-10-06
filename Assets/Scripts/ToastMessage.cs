// using System;
// using UnityEngine;
//
// public class ToastMessage : Singleton<ToastMessage>
// {
//     private AndroidJavaObject _context;
//
//     private AndroidJavaObject _currentActivity;
//
//     private string _input;
//     private string _toastString;
//
//     private AndroidJavaClass _unityPlayer;
//
//     private void Start()
//     {
//         if (Application.platform == RuntimePlatform.Android)
//         {
//             _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//             _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//             _context = _currentActivity.Call<AndroidJavaObject>("getApplicationContext", Array.Empty<object>());
//         }
//     }
//
//     public static void ShowToastOnUiThread(string toastString)
//     {
//         if (Application.platform == RuntimePlatform.Android)
//         {
//             Instance._toastString = toastString;
//             Instance._currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(Instance.ShowToast));
//         }
//     }
//
//     private void ShowToast()
//     {
//         Debug.Log(this + ": Running on UI thread");
//         var androidJavaClass = new AndroidJavaClass("android.widget.Toast");
//         var androidJavaObject = new AndroidJavaObject("java.lang.String", _toastString);
//         androidJavaClass.CallStatic<AndroidJavaObject>("makeText", _context, androidJavaObject,
//             androidJavaClass.GetStatic<int>("LENGTH_SHORT")).Call("show");
//     }
// }