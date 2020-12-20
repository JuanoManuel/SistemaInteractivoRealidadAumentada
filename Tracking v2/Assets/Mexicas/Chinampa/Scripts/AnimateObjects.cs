using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjects : MonoBehaviour
{
    public Animator[] objects;

    private Animator anim;
    public bool currentValue = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerObjectsBool(string boolName = "IsActive")
    {
        if (objects != null)
        {
            currentValue = !currentValue;
            foreach (Animator anim in objects)
                anim.SetBool(boolName, currentValue);
        }
    }
}
