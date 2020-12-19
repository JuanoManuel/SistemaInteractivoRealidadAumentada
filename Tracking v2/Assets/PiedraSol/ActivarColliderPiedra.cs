using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarColliderPiedra : MonoBehaviour
{
    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void ActivarCollider()
    {
        col.enabled = true;
    }

    public void DesactivarCollider()
    {
        col.enabled = false;
    }
}
