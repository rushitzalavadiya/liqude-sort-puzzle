using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FillScreen : MonoBehaviour
{
	private SpriteRenderer _renderer;

	private Camera _camera;

	private void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_camera = Camera.main;
	}

	private void Start()
	{
	}

	private void Update()
	{
		UpdateFillScreen();
	}

	private void UpdateFillScreen()
	{
		float num = _camera.orthographicSize * 2f;
		float d = num * (float)Screen.width / (float)Screen.height;
		float num2 = (float)Screen.width / (float)Screen.height;
		float num3 = _renderer.sprite.bounds.extents.x / _renderer.sprite.bounds.extents.y;
		if (num2 >= num3)
		{
			base.transform.localScale = Vector3.one * d / (2f * _renderer.sprite.bounds.extents.x);
		}
		else
		{
			base.transform.localScale = Vector3.one * num / (2f * _renderer.sprite.bounds.extents.y);
		}
	}
}
