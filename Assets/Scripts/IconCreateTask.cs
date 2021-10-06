using UnityEngine;

public class IconCreateTask : CustomYieldInstruction
{
	public override bool keepWaiting => !Completed;

	public bool Completed
	{
		get;
		private set;
	}

	public string Error
	{
		get;
		private set;
	}

	public Texture2D Texture
	{
		get;
		private set;
	}

	public string Path
	{
		get;
		private set;
	}

	public void Complete(Texture2D texture, string path)
	{
		Texture = texture;
		Path = path;
		Completed = true;
	}

	public void Complete(string error)
	{
		Error = error;
		Completed = true;
	}
}
