using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    MenuBehavour menuBehavour;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        menuBehavour = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuBehavour>();        
    }

    private void Start()
    {
        gameObject.name = GetComponent<Image>().sprite.name;
    }

    public void UpdateLevel()
    {
        if (!gameObject.name.Equals(menuBehavour.GetLevelSelected()))
        {
            animator.SetBool("Chosen", true);
            menuBehavour.SetLevel(gameObject.name, animator);
        }
    }
}
