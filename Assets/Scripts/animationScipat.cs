using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScipat : MonoBehaviour
{
    public GameObject animation;

    public void Play()
    {
        animation.GetComponent<Animator>().enabled = true;
    }
}
