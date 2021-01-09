using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesplayAros : MonoBehaviour
{
    private Vector2 touchPosition = default;
    private Camera arCamera;
    public Animator Aro1;
    public Animator Aro2;
    public Animator Aro3;
    public Animator Aro4;
    public Animator Aro567;
    public Animator Aro8;
    public Animator Piedra;
    public Animator titleToClose;
    public bool closeAfterOpenWindow;
    private static bool isDeploying;

    private void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        isDeploying = false;
    }

    private void Start()
    {
        DesplayMoreInformation.SetDeploy(true);
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
                    if (hitObject.collider == gameObject.GetComponent<Collider>())
                    {
                        Aro1.SetBool("IsActive", true);
                        Aro1.SetBool("Mover", true);


                        Aro2.SetBool("IsActive", true);
                        Aro2.SetBool("Mover", true);


                        Aro3.SetBool("IsActive", true);
                        Aro3.SetBool("Mover", true);


                        Aro4.SetBool("IsActive", true);
                        Aro4.SetBool("Mover", true);


                        Aro567.SetBool("IsActive", true);
                        Aro567.SetBool("Mover", true);

                        Aro8.SetBool("IsActive", true);
                        Aro8.SetBool("Mover", true);

                        Piedra.SetBool("IsActive", false);


                        isDeploying = true;
                        DesplayMoreInformation.SetDeploy(false);
                        if (closeAfterOpenWindow)
                            titleToClose.SetBool("Mover", false);
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
                    Aro1.SetBool("IsActive", true);
                    Aro1.SetBool("Mover", true);


                    Aro2.SetBool("IsActive", true);
                    Aro2.SetBool("Mover", true);


                    Aro3.SetBool("IsActive", true);
                    Aro3.SetBool("Mover", true);


                    Aro4.SetBool("IsActive", true);
                    Aro4.SetBool("Mover", true);


                    Aro567.SetBool("IsActive", true);
                    Aro567.SetBool("Mover", true);

                    Aro8.SetBool("IsActive", true);
                    Aro8.SetBool("Mover", true);

                    Piedra.SetBool("IsActive", false);
                    DesplayMoreInformation.SetDeploy(false);
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

    public void CloseInformationWindow()
    {
        Aro1.SetBool("Mover", true);
        Aro2.SetBool("Mover", false);
        Aro3.SetBool("Mover", false);
        Aro4.SetBool("Mover", false);
        Aro567.SetBool("Mover", false);
        Aro8.SetBool("Mover", false);
        isDeploying = false;
    }
}
