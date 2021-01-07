using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAnimationController : MonoBehaviour
{
    public string animatorVariableName = "IsActive";
    public float secondsToActivate;
    public float secondsWhenIsActive;

    private Animator anim;
    public float timer = 0f,totalSecondsToActivate;
    private bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        totalSecondsToActivate = secondsToActivate + secondsWhenIsActive;
    }
    private void Update()
    {
        if (!isActive)
        {
            if (timer >= secondsToActivate)
            {
                anim.SetBool(animatorVariableName, true);
                isActive = true;
            }
        }
        else
        {
            if(timer >= totalSecondsToActivate)
            {
                anim.SetBool(animatorVariableName, false);
                isActive = false;
                timer = secondsWhenIsActive * -1f;
            }
        }

            timer += Time.deltaTime;
    }
}
