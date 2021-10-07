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
        transform.Rotate(600f * Time.deltaTime, 0f, 0f);
    }
}