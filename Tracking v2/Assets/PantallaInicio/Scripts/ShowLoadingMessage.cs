using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLoadingMessage : MonoBehaviour
{
    public bool flagCultura;

    GameObject controller;
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }
    public void CargarEscenaTracking()
    {
        if (flagCultura)
            OpenScene("ARTeotihuacan");
        else
            OpenScene("ARMexicas");
    }

    void OpenScene(string sceneName)
    {
        IdiomaAdmin.creado = false;
        Destroy(controller);
        SceneManager.LoadScene(sceneName);
    }
}
