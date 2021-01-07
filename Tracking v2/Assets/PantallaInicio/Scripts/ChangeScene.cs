using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void cambiarEscenaInicio_SelecCultura(){
        SceneManager.LoadScene("SelecCultura");
    }

    public void cambiarEscenaSelecCultura_Inicio(){
        SceneManager.LoadScene("MenuPrincipal");
    }

    //Menu de ayuda de principal a menu principal
    public void cambiarEscenaMenu_Ayuda_Principal(){
    	SceneManager.LoadScene("MenuPrincipal");
    }

    //Menu principal a su menu de ayuda
    public void cambiarEscenaMenu_Principal_Ayuda(){
    	SceneManager.LoadScene("AyudaMenuPrincipal");
    }

    //Menu principal a menu de configuración
    public void cambiarEscenaMenu_Principal_Menu_Configuracion(){
    	SceneManager.LoadScene("Menu");
    }

    //Menu de configuracion a menu de ayuda
    public void cambiarEscenaMenu_Ayuda(){
    	SceneManager.LoadScene("Ayuda");
    }
    public void cambiarEscena(){
		SceneManager.LoadScene("Menu");
	}

}
