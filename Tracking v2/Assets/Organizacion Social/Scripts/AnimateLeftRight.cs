using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimateLeftRight : MonoBehaviour
{

    public Animator nextObject;
    public string paramName;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerNextBool()
    {
        if(nextObject != null)
            nextObject.SetBool(paramName, !nextObject.GetBool(paramName));
    }

    public void TriggerNextTrigger(string triggerName)
    {
        if (nextObject != null)
            nextObject.SetTrigger(triggerName);
    }

    
}
