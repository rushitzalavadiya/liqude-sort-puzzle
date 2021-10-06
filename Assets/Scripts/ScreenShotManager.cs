// using System;
// using System.Collections;
// using System.IO;
// using UnityEngine;
//
// public class ScreenShotManager : Singleton<ScreenShotManager>
// {
//     private void LateUpdate()
//     {
//         if (UnityEngine.Input.GetKeyDown(KeyCode.C))
//         {
//             StartCoroutine(TakeScreenShotCo());
//         }
//     }
//
//     private IEnumerator TakeScreenShotCo()
//     {
//         yield return new WaitForEndOfFrame();
//         ScreenCapture.CaptureScreenshot(
//             Path.Combine(new DirectoryInfo(Application.persistentDataPath).Parent.FullName,
//                 $"Screenshot_{DateTime.Now:yyyyMMdd_Hmmss}.png"), 1);
//         UnityEngine.Debug.Log("Screen Shot Captured");
//     }
// }