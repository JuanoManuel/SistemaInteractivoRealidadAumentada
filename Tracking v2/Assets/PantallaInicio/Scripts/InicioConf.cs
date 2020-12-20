using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class InicioConf : MonoBehaviour{
	[TextArea(5, 20)]
	public string español;
	[TextArea(5,20)]
	public string ingles;
	private IdiomaAdmin idiomadmin;
 	public GameSettings gameSettings;

	void Awake() {
		
	}

	void Start(){
		idiomadmin = GameObject.FindGameObjectWithTag("GameController").GetComponent<IdiomaAdmin>();
		LoadSettings();
	}

	public void LoadSettings(){
        if (idiomadmin.SelecIdioma() == "español")
        {
			//GetComponentInChildren<Text>().text = español;
			Text text = GetComponentInChildren<Text>();
			if (text != null)
				text.text = español;
            else
            {
				TextMeshPro text2 = GetComponentInChildren<TextMeshPro>();
				if (text2 != null)
					text2.text = español;
            }
        }else {
			//GetComponentInChildren<Text>().text = ingles;
			Text text = GetComponentInChildren<Text>();
			if (text != null)
			{
				text.text = ingles;

			}
			else
			{
				TextMeshPro text2 = GetComponentInChildren<TextMeshPro>();
				if (text2 != null)
					text2.text = ingles;
			}
		}
	}
}
