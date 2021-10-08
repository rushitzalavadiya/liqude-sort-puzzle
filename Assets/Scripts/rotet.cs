using UnityEngine;

public class rotet : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0f, 0f, 100f * Time.deltaTime);
    }
}