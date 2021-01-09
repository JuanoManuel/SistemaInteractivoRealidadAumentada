using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text textMovimientos;
    int numMovimientos;

    public void SumarMovimiento()
    {
        numMovimientos++;
        textMovimientos.text = numMovimientos.ToString();
    }

    public void ReiniciarMovimientos()
    {
        numMovimientos = 0;
        textMovimientos.text = numMovimientos.ToString();
    }
}
