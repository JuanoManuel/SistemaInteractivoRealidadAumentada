using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesplayMoreInformation : MonoBehaviour
{
    private Vector2 touchPosition = default;
    private Camera arCamera;
    public Animator panelInformationAnimator;
    public Animator titleToClose;
    public bool closeAfterOpenWindow;
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
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider == gameObject.GetComponent<Collider>())
                    {
                        panelInformationAnimator.SetBool("IsActive", true);
                        isDeploying = true;
                        if (closeAfterOpenWindow)
                            titleToClose.SetBool("IsActive", false);
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
                if (hitObject.collider == gameObject.GetComponent<Collider>())
                {
                    panelInformationAnimator.SetBool("IsActive", true);
                    isDeploying = true;
                    if (closeAfterOpenWindow)
                    {
                        isDeploying = false;
                        titleToClose.SetBool("IsActive", false);
                    }
                }
            }
        }
    }

    public static bool IsDeployed()
    {
        return isDeploying;
    }

    public static void SetDeploy(bool value)
    {
        isDeploying = value;
    }

    public void CloseInformationWindow()
    {
        panelInformationAnimator.SetBool("IsActive", false);
        isDeploying = false;
    }
}
