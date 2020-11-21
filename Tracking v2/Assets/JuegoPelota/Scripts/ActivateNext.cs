using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNext : MonoBehaviour
{

    public Animator nextArrow;
    private bool currentState;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentState = nextArrow.GetBool("IsActive");
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsActive") != currentState){
            currentState = !currentState;
            nextArrow.SetBool("IsActive", currentState);

        }
    }
}
