using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
	Vector3 screenSpace, offset, posInicial;
	Sensores sensores;
	Puzzle puzzle;
    Camera arCamera;
	bool moviendoLeft, moviendoRight, moviendoUp, moviendoDown;
    UI ui;
    Text debugger;

    private void Awake()
    {
        sensores = GetComponentInChildren<Sensores>();
        puzzle = GameObject.Find("GameController").GetComponent<Puzzle>();
        ui = GameObject.Find("GameController").GetComponent<UI>();
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        debugger = GameObject.FindGameObjectWithTag("Debugger").GetComponent<Text>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = arCamera.ScreenToWorldPoint(touch.position);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        screenSpace = arCamera.WorldToScreenPoint(transform.position);
                        offset = transform.position - arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                        posInicial = transform.position;
                        break;
                    case TouchPhase.Moved:
                        Vector3 curScreenSpace = new Vector3(touch.position.x, touch.position.y, 0);
                        Vector3 temp = arCamera.ScreenToWorldPoint(curScreenSpace);
                        Vector3 curPosition = temp + offset;
                        debugger.text = gameObject.name;
                        if (!sensores.left || !sensores.right)
                        {                           //Movimiento horizontal
                            curPosition = new Vector3(curPosition.x, transform.position.y, 0);
                            if (!sensores.left && !moviendoLeft && !moviendoRight)
                            {
                                moviendoLeft = true;
                            }
                            if (!sensores.right && !moviendoLeft && !moviendoRight)
                            {
                                moviendoRight = true;
                            }
                        }
                        else if (!sensores.up || !sensores.down)
                        {                       //Movimiento vertical
                            curPosition = new Vector3(transform.position.x, curPosition.y, 0);
                            if (!sensores.up && !moviendoUp && !moviendoDown)
                            {
                                moviendoUp = true;
                            }
                            if (!sensores.down && !moviendoUp && !moviendoDown)
                            {
                                moviendoDown = true;
                            }
                        }
                        else
                        {
                            return;
                        }

                        if (moviendoLeft)
                        {
                            if (curPosition.x > posInicial.x)
                            {
                                return;
                            }
                        }
                        if (moviendoRight)
                        {
                            if (curPosition.x < posInicial.x)
                            {
                                return;
                            }
                        }
                        if (moviendoUp)
                        {
                            if (curPosition.y < posInicial.y)
                            {
                                return;
                            }
                        }
                        if (moviendoDown)
                        {
                            if (curPosition.y > posInicial.y)
                            {
                                return;
                            }
                        }

                        if (Vector3.Distance(curPosition, posInicial) > 1) return;

                        transform.position = curPosition;
                        break;
                    case TouchPhase.Ended:
                        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
                        if (transform.position != posInicial)
                        {
                            ui.SumarMovimiento();
                            puzzle.ComprobarGanador();
                        }


                        moviendoLeft = false;
                        moviendoRight = false;
                        moviendoUp = false;
                        moviendoDown = false;
                        break;

                }
            }
            
        }
    }

    //private void OnMouseDown()
    //{
    //    screenSpace = arCamera.WorldToScreenPoint(transform.position);
    //    offset = transform.position - arCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    //    posInicial = transform.position;
    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    //    Vector3 temp = arCamera.ScreenToWorldPoint(curScreenSpace);
    //    Vector3 curPosition = temp + offset;

    //    if (!sensores.left || !sensores.right)
    //    {                           //Movimiento horizontal
    //        curPosition = new Vector3(curPosition.x, transform.position.y, 0);
    //        if (!sensores.left && !moviendoLeft && !moviendoRight)
    //        {
    //            moviendoLeft = true;
    //        }
    //        if (!sensores.right && !moviendoLeft && !moviendoRight)
    //        {
    //            moviendoRight = true;
    //        }
    //    }
    //    else if (!sensores.up || !sensores.down)
    //    {                       //Movimiento vertical
    //        curPosition = new Vector3(transform.position.x, curPosition.y, 0);
    //        if (!sensores.up && !moviendoUp && !moviendoDown)
    //        {
    //            moviendoUp = true;
    //        }
    //        if (!sensores.down && !moviendoUp && !moviendoDown)
    //        {
    //            moviendoDown = true;
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }

    //    if (moviendoLeft)
    //    {
    //        if (curPosition.x > posInicial.x)
    //        {
    //            return;
    //        }
    //    }
    //    if (moviendoRight)
    //    {
    //        if (curPosition.x < posInicial.x)
    //        {
    //            return;
    //        }
    //    }
    //    if (moviendoUp)
    //    {
    //        if (curPosition.y < posInicial.y)
    //        {
    //            return;
    //        }
    //    }
    //    if (moviendoDown)
    //    {
    //        if (curPosition.y > posInicial.y)
    //        {
    //            return;
    //        }
    //    }

    //    if (Vector3.Distance(curPosition, posInicial) > 1) return;

    //    transform.position = curPosition;
    //}

    //void OnMouseUp()
    //{
    //    transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    //    if (transform.position != posInicial)
    //    {
    //        ui.SumarMovimiento();
    //        puzzle.ComprobarGanador();
    //    }


    //    moviendoLeft = false;
    //    moviendoRight = false;
    //    moviendoUp = false;
    //    moviendoDown = false;

    //}
}
