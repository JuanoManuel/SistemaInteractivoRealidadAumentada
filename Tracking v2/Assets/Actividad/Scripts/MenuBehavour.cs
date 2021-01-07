using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavour : MonoBehaviour
{
    public string levelSelected;
    public ChangeDificulty.Modes modeSelected;
    [Header("Objetos a mostrar-ocultar")]
    public Animator settings;
    public Animator panel;
    public Animator startButton;
    public Animator puzzleButtons;

    Puzzle puzzle;
    Animator levelSelectedAnimator,modeSelectedAnimator;

    private void Awake()
    {
        puzzle = GameObject.FindGameObjectWithTag("GameController").GetComponent<Puzzle>();
    }

    public void SetLevel(string levelname,Animator newAnimator)
    {
        levelSelected = levelname;
        if (levelSelectedAnimator != null)
            levelSelectedAnimator.SetBool("Chosen", false);
        levelSelectedAnimator = newAnimator;
    }

    public void SetDificultad(ChangeDificulty.Modes dificultad, Animator newAnimator)
    {
        modeSelected = dificultad;
        if (modeSelectedAnimator != null)
            modeSelectedAnimator.SetBool("Chosen", false);
        modeSelectedAnimator = newAnimator;
    }

    public void StartGame()
    {
        //comenzamos el juego
        if (puzzle.BuildGame(levelSelected, modeSelected))
        {
            //ocultamos y desactivamos UI
            settings.SetBool("Enabled", false);
            panel.SetBool("Enabled", false);
            startButton.SetBool("Enabled", false);
            //mostramos botones de puzzle
            puzzleButtons.SetBool("Enabled", true);
        }else
            print("Error al iniciar el juego");
    }

    public void Withdraw()
    {
        //borramos toda instancia del puzzle
        puzzle.Rendirse();
        //ocultamos interface puzzle
        puzzleButtons.SetBool("Enabled",false);
        //mostramos ui menu
        settings.SetBool("Enabled", true);
        panel.SetBool("Enabled", true);
        startButton.SetBool("Enabled", true);
    }

    public void PlayAgain()
    {
        puzzle.JuegarDeNuevo();
        //ocultamos interface puzzle
        puzzleButtons.SetBool("Enabled", false);
        //mostramos ui menu
        settings.SetBool("Enabled", true);
        panel.SetBool("Enabled", true);
        startButton.SetBool("Enabled", true);

    }

    public string GetLevelSelected()
    {
        return levelSelected;
    }

    public ChangeDificulty.Modes GetModeSelected()
    {
        return modeSelected;
    }
}
