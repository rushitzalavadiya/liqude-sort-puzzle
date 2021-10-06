using UnityEngine;

public class Liquid : MonoBehaviour
{
	[SerializeField]
	private int _groupId;

	[SerializeField]
	private SpriteRenderer _renderer;

	[SerializeField]
	private SpriteRenderer _bottomRenderer;

	[SerializeField]
	private float _bottomRendererSize;

	[SerializeField]
	private float _unitSize;

	[SerializeField]
	private Color[] _groupColors = new Color[0];

	private float _value;

	private bool _isBottomLiquid;

	public int GroupId
	{
		get
		{
			return _groupId;
		}
		set
		{
			_groupId = value;
			_renderer.color = _groupColors[value];
			_bottomRenderer.color = _groupColors[value];
		}
	}

	public SpriteRenderer Renderer => _renderer;

	public bool IsBottomLiquid
	{
		get
		{
			return _isBottomLiquid;
		}
		set
		{
			_isBottomLiquid = value;
			Value = Value;
		}
	}

	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (IsBottomLiquid && (double)value > 0.9)
			{
				_bottomRenderer.gameObject.SetActive(value: true);
				_renderer.transform.localPosition = _renderer.transform.localPosition.WithY(_bottomRendererSize);
				_renderer.transform.localScale = _renderer.transform.localScale.WithY(_unitSize * value - _bottomRendererSize);
			}
			else
			{
				_bottomRenderer.gameObject.SetActive(value: false);
				_renderer.transform.localPosition = Vector3.zero;
				_renderer.transform.localScale = _renderer.transform.localScale.WithY(_unitSize * value);
			}
			_value = value;
		}
	}

	public float Size => Value * _unitSize;
}
