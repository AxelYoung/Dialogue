using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOptimization : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    private void OnBecameVisible()
    {
        gameObject.GetComponent<Animator>().enabled = true;
    }
}
