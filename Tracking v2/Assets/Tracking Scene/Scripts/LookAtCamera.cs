using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera arCamera;
    private void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        gameObject.transform.LookAt(arCamera.transform);   
    }
}
