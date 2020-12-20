using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador3Controller : MonoBehaviour
{
    public float secondsToActivateAnimation;
    public Animator characterAnimator;

    private Animator animator;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsActive")) {
            characterAnimator.SetBool("IsActive", true);
            timer = 0;
        }
        else
        {
            characterAnimator.SetBool("IsActive", false);
            if (timer >= secondsToActivateAnimation)
            {
                characterAnimator.SetTrigger("Exercise");
                timer = -7f;
            }
            timer += Time.deltaTime;
        }
    }
}
