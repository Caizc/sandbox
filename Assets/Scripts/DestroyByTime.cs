using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float Lifetime = 10.0f;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }
}