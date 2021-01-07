using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonBehavour : MonoBehaviour
{
    public GameObject startButton;

    public void ActivarBoton()
    {
        startButton.SetActive(true);
    }

    public void DesactivarBoton()
    {
        startButton.SetActive(false);
    }
}
