using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class FillScreen : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private Camera _camera;

    public Sprite[] bgs;

    public Image[] bgs2;

    public GameObject backpanel;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (bgs.Length != 0)
        {
            int okk =Random.Range(0, bgs.Length);
            _renderer.sprite = bgs[okk];
           // backpanel.GetComponent<Image>().sprite= bgs[Random.Range(0, bgs.Length)];
           backpanel.GetComponent<UnityEngine.UI.Image>().sprite=bgs[okk];
        }

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