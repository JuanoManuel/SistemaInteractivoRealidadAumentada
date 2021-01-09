using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBetweenModels : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nextModel;
    public bool active;

    private void Update()
    {
        if (!active)
        {
            nextModel.SetActive(true);
            gameObject.SetActive(false);
            active = true;
        }
    }

}
