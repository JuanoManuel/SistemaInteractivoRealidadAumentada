using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivitiesManager : MonoBehaviour
{
    private GameObject controller;
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    public void OpenTeotihuacanActivity()
    {
        OpenScene("ActividadTeotihuacan");
    }

    public void OpenMexicasActivity()
    {
        OpenScene("ActividadMexicas");
    }

    public void OpenTeotihuacan()
    {
        OpenScene("ARTeotihuacan");
    }

    public void OpenMexicas()
    {
        OpenScene("ARMexicas");
    }

    public void OpenMenuPrincipal()
    {
        OpenScene("MenuPrincipal");
    }

    void OpenScene(string sceneName)
    {
        IdiomaAdmin.creado = false;
        Destroy(controller);
        SceneManager.LoadScene(sceneName);
    }
}
