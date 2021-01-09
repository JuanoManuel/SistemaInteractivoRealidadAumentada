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

    //Debugger variables
    GameObject debugger;
    Text nameFicha,statusSensores, positions, dirMovement, distance;

    private void Awake()
    {
        sensores = GetComponentInChildren<Sensores>();
        puzzle = GameObject.Find("GameController").GetComponent<Puzzle>();
        ui = GameObject.Find("GameController").GetComponent<UI>();
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        debugger = GameObject.FindGameObjectWithTag("Debugger");
        if (debugger)
        {
            nameFicha = debugger.transform.GetChild(0).gameObject.GetComponent<Text>();
            statusSensores = debugger.transform.GetChild(1).gameObject.GetComponent<Text>();
            positions = debugger.transform.GetChild(2).gameObject.GetComponent<Text>();
            dirMovement = debugger.transform.GetChild(3).gameObject.GetComponent<Text>();
            distance = debugger.transform.GetChild(4).gameObject.GetComponent<Text>();
        }
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
                        if(nameFicha)
                            nameFicha.text = gameObject.name;
                        if(statusSensores)
                        statusSensores.text = "Left: " + sensores.left + "Right: " + sensores.right + "Up: " + sensores.up + "Down: " + sensores.down;
                        if (!sensores.left || !sensores.right)
                        {                           //Movimiento horizontal
                            curPosition = new Vector3(curPosition.x, transform.position.y, 0);
                            if (!sensores.left && !moviendoLeft && !moviendoRight)
                            {
                                moviendoLeft = true;
                                if(dirMovement)
                                dirMovement.text = "Moviendo left";
                            }
                            if (!sensores.right && !moviendoLeft && !moviendoRight)
                            {
                                moviendoRight = true;
                                if (dirMovement)
                                    dirMovement.text = "Moviendo right";
                            }
                        }
                        else if (!sensores.up || !sensores.down)
                        {                       //Movimiento vertical
                            curPosition = new Vector3(transform.position.x, curPosition.y, 0);
                            if (!sensores.up && !moviendoUp && !moviendoDown)
                            {
                                moviendoUp = true;
                                if (dirMovement)
                                    dirMovement.text = "Moviendo up";
                            }
                            if (!sensores.down && !moviendoUp && !moviendoDown)
                            {
                                moviendoDown = true;
                                if (dirMovement)
                                    dirMovement.text = "Moviendo down";
                            }
                        }
                        else
                        {
                            return;
                        }
                        if(positions)
                        positions.text = "inicial: " + posInicial + "actual: " + curPosition;

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
                        if(distance)
                        distance.text = Vector3.Distance(curPosition, posInicial)+"";
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

    private void OnMouseDown()
    {
        screenSpace = arCamera.WorldToScreenPoint(transform.position);
        offset = transform.position - arCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        posInicial = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 temp = arCamera.ScreenToWorldPoint(curScreenSpace);
        Vector3 curPosition = temp + offset;

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
    }

    void OnMouseUp()
    {
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

    }
}
