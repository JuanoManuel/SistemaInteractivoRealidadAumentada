using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInteractives : MonoBehaviour
{
    public GameObject model;

    private Vector2 touchPosition;
    private Camera arCamera;

    private void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider == gameObject.GetComponent<MeshCollider>())
                    {
                        model.GetComponent<SwitchBetweenModels>().active = false;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.collider == gameObject.GetComponent<MeshCollider>())
                {
                    model.GetComponent<SwitchBetweenModels>().active = false;
                }
            }
        }
    }
}
