using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ayuda : MonoBehaviour
{
	public Toggle toggleEspañol;
	public Toggle toggleIngles;
	public string español;
	public string ingles;
	public GameObject objeto;

	void Start(){
		objeto.SetActive(false);
	}

	void OnEnable(){

		toggleEspañol.onValueChanged.AddListener(delegate{cambiaFrasePrueba();});

	}

	public void cambiaFrasePrueba(){
		if(toggleEspañol.isOn){
			GetComponentInChildren<Text>().text=español;
			objeto.SetActive(false);
		}else if(toggleIngles.isOn){
			GetComponentInChildren<Text>().text=ingles;
			objeto.SetActive(true);
		}

	}
    
}
