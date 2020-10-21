using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesplayMoreInformation : MonoBehaviour
{
    private Vector2 touchPosition = default;
    private Camera arCamera;
    public Animator panelInformationAnimator;
    public string panelName;
    private static bool isDeploying;

    private void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        isDeploying = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && !isDeploying)
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
                        panelInformationAnimator.SetBool("IsActive", true);
                        isDeploying = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && !isDeploying)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.collider == gameObject.GetComponent<MeshCollider>())
                {
                    panelInformationAnimator.SetBool("IsActive", true);
                    isDeploying = true;
                }
            }
        }
    }

    public void CloseInformationWindow()
    {
        panelInformationAnimator.SetBool("IsActive", false);
        isDeploying = false;
    }
}
