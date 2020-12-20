using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTour : MonoBehaviour
{

    private GameObject controller;
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    public void OpenTeotihuacan()
    {
        IdiomaAdmin.creado = false;
        Destroy(controller);
        SceneManager.LoadScene("ARTeotihuacan");
    }

    public void OpenMexicas()
    {
        IdiomaAdmin.creado = false;
        Destroy(controller);
        SceneManager.LoadScene("ARMexicas");
    }

    public void OpenMainMenu()
    {
        IdiomaAdmin.creado = false;
        Destroy(controller);
        SceneManager.LoadScene("MenuPrincipal");
    }
}
