using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraSized : MonoBehaviour
{
    public float fixedOrthographicSize = 5.0f; // Tamaño fijo deseado

    void Start()
    {
        Camera.main.orthographicSize = fixedOrthographicSize;
    }
}
