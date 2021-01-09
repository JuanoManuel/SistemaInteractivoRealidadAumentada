using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensores : MonoBehaviour
{
    public GameObject sensorIzq, sensorDer, sensorUp, sensorDown;
    public float radioSensor = 1.0f;
    [Header("Ocupado")]
    public bool left;
    public bool right;
    public bool up;
    public bool down;
    private void FixedUpdate()
    {
        Comprobar();
    }

    void Comprobar()
    {
        left = Physics2D.OverlapCircle(sensorIzq.transform.position, radioSensor);
        right = Physics2D.OverlapCircle(sensorDer.transform.position, radioSensor);
        up = Physics2D.OverlapCircle(sensorUp.transform.position, radioSensor);
        down = Physics2D.OverlapCircle(sensorDown.transform.position, radioSensor);
    }
}
