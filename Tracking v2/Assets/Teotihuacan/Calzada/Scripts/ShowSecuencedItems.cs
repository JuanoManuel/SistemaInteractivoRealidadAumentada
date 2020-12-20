using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShowSecuencedItems : MonoBehaviour
{
    public float delayBetweenSpawns;
    public Animator mainArrow;
    public Animator[] items;

    private bool currentState;
    private void Start()
    {
        currentState = mainArrow.GetBool("IsActive");
    }
    // Update is called once per frame
    void Update()
    {
        if(currentState != mainArrow.GetBool("IsActive"))
        {
            currentState = !currentState;
            StartCoroutine(SpawnObjects());
        }
    }

    private IEnumerator SpawnObjects()
    {
        foreach (Animator anim in items)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);
            anim.SetBool("IsActive", currentState);
        }
    }
}
