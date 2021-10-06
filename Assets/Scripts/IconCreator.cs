using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Camera))]
public class IconCreator : MonoBehaviour
{
	public Camera Camera => GetComponent<Camera>();

	public Vector2 Size
	{
		get
		{
			return new Vector2(Camera.orthographicSize * 2f * Camera.aspect, Camera.orthographicSize * 2f);
		}
		set
		{
			Camera.orthographicSize = value.y / 2f;
			Camera.aspect = value.x / value.y;
		}
	}

	public LayerMask LayerMask
	{
		get
		{
			return Camera.cullingMask;
		}
		set
		{
			Camera.cullingMask = value;
		}
	}

	public string FileName
	{
		get;
		set;
	}

	public static string CreateIcon(GameObject go, Vector2 size, LayerMask layerMask, string fileName)
	{
		IconCreator iconCreator = UnityEngine.Object.Instantiate(Resources.Load<IconCreator>("IconCreator"), go.transform.position - Vector3.forward * 3f, Quaternion.identity);
		iconCreator.transform.LookAt(go.transform);
		iconCreator.Size = size;
		iconCreator.LayerMask = layerMask;
		iconCreator.FileName = fileName;
		string result = iconCreator.TakeScreenShot();
		UnityEngine.Object.Destroy(iconCreator.gameObject);
		return result;
	}

	private string TakeScreenShot()
	{
		try
		{
			int pixelHeight = Camera.pixelHeight;
			int num = (int)(Camera.aspect * (float)pixelHeight);
			RenderTexture renderTexture = new RenderTexture(num, pixelHeight, 24, RenderTextureFormat.ARGB32);
			Camera.targetTexture = renderTexture;
			Texture2D texture2D = new Texture2D(num, pixelHeight, TextureFormat.ARGB32, mipChain: false);
			Camera.Render();
			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0f, 0f, num, pixelHeight), 0, 0);
			Camera.targetTexture = null;
			RenderTexture.active = null;
			renderTexture.Release();
			UnityEngine.Object.Destroy(renderTexture);
			File.WriteAllBytes(bytes: texture2D.EncodeToPNG(), path: FileName);
			UnityEngine.Object.Destroy(texture2D);
			return FileName;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
			return "";
		}
	}

	public static IEnumerator LoadIconFromFileAsync(string path, Action<Texture2D> completed = null)
	{
		using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(path ?? ""))
		{
			yield return request.SendWebRequest();
			if (!string.IsNullOrEmpty(request.error))
			{
				completed?.Invoke(null);
			}
			else
			{
				Texture2D content = DownloadHandlerTexture.GetContent(request);
				if (!(content == null))
				{
					completed?.Invoke(content);
					yield break;
				}
				completed?.Invoke(null);
			}
		}
		/*Error near IL_00ed: Unexpected return in MoveNext()*/;
	}
}
