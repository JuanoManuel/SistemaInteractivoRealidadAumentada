using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarColliderPiedra : MonoBehaviour
{
    public Collider col;

    public void ActivarCollider()
    {
        col.enabled = true;
    }

    public void DesactivarCollider()
    {
        col.enabled = false;
    }
}
