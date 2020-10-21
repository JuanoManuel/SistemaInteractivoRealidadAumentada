using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    public Animator animator;

    public void CloseCanvas()
    {
        animator.SetBool("IsActive", false);
    }

    public void OpenCanvas()
    {
        animator.SetBool("IsActive", false);
    }
    
}
