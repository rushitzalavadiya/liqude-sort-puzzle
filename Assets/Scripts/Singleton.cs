using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	public static T Instance
	{
		get;
		private set;
	}

	protected virtual void Awake()
	{
		if ((Object)Instance == (Object)null)
		{
			Instance = (T)this;
			Object.DontDestroyOnLoad(base.gameObject);
			OnInit();
		}
		else if (Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	protected virtual void OnInit()
	{
	}
}
