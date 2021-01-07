using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimateNext2 : MonoBehaviour
{

    public Animator nextObject;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerNextBool(string boolName = "IsActive")
    {
        if(nextObject != null)
            nextObject.SetBool(boolName, anim.GetBool(boolName));
    }

    public void TriggerNextTrigger(string triggerName)
    {
        if (nextObject != null)
            nextObject.SetTrigger(triggerName);
    }

    
}
