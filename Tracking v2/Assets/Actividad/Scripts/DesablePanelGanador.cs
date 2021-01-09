using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesablePanelGanador : MonoBehaviour
{
    public GameObject panelGanador;

    public void DesablePanel()
    {
        panelGanador.SetActive(false);
    }
}
