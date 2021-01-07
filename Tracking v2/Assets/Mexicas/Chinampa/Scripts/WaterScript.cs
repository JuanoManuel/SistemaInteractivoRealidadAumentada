using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public Animator rootAnimator;
    public void ChangeRootColor(string name)
    {
        if(rootAnimator != null)
            if (rootAnimator.GetBool("IsActive"))
                rootAnimator.SetTrigger(name);
    }
}
