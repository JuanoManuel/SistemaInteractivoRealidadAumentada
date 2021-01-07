using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public GameObject instantiate;
    public TextMeshPro text;
    public GameObject nextArrow, previousArrow;
    [TextArea(3,10)]
    public string[] textos;

    private int maxPages, currentPage;
    private Vector2 touchPosition;
    private Camera arCamera;

    public void Awake()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void Start()
    {
        maxPages = textos.Length;
        currentPage = 0;
        text.text = textos[0];
        previousArrow.SetActive(false);
    }

    private void Update()
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
                    if (hitObject.collider == nextArrow.GetComponent<MeshCollider>())
                    {
                        ShowNextPage();
                    }
                    else if(hitObject.collider == previousArrow.GetComponent<MeshCollider>())
                    {
                        //Instantiate(instantiate, hitObject.point, Quaternion.identity);
                        ShowPreviousPage();
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
                if (hitObject.collider == nextArrow.GetComponent<MeshCollider>())
                {
                    ShowNextPage();
                }
                else if (hitObject.collider == previousArrow.GetComponent<MeshCollider>())
                {
                    ShowPreviousPage();
                }
            }
        }
    }
    public void ShowNextPage() 
    {
        if (currentPage == 0)
            previousArrow.SetActive(true);
        currentPage++;
        text.text = textos[currentPage];
        if(currentPage+1 >= maxPages)
        {
            nextArrow.SetActive(false);
        }               
    }

    public void ShowPreviousPage()
    {
        if (currentPage == maxPages - 1)
            nextArrow.SetActive(true);
        currentPage--;
        text.text = textos[currentPage];
        if(currentPage <= 0)
        {
            previousArrow.SetActive(false);
        }
    }
}
