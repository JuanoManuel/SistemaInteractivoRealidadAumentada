using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool xAxis = true, yAxis = true, zAxis = true;

    private Camera arCamera;
    private void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        gameObject.transform.LookAt(new Vector3(xAxis ? arCamera.transform.position.x : transform.position.x,
            yAxis ? arCamera.transform.position.y : transform.position.y, zAxis ? arCamera.transform.position.z : transform.position.z));
    }
}
