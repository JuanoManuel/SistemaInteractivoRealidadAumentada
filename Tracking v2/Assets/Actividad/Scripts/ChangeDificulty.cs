using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDificulty : MonoBehaviour
{

    MenuBehavour menuBehavour;
    Animator animator;
    public enum Modes { Easy, Normal, Hard, Nothing}
    public Modes dificultad;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        menuBehavour = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuBehavour>();
    }

    public void UpdateDificultad()
    {
        if (!dificultad.Equals(menuBehavour.GetModeSelected()))
        {
            animator.SetBool("Chosen", true);
            menuBehavour.SetDificultad(dificultad, animator);
        }
    }
}
