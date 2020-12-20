using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Configuracion : MonoBehaviour{
	public Slider musicVolumeSlider;
	public Button applyButton;
	public string español;
	public string ingles;
	public Toggle Español;
	public Toggle Ingles;
	public Toggle Apagados;
	public Toggle IEspañol;
	public Toggle IIngles;
	private IdiomaAdmin idiomadmin;

	public GameSettings gameSettings;


	void Awake()
	{
		idiomadmin = GameObject.FindGameObjectWithTag("GameController").GetComponent<IdiomaAdmin>();
	}

	void OnEnable(){
		gameSettings = new GameSettings();
		IIngles.onValueChanged.AddListener(delegate{OnIdiomaChange();});
		IEspañol.onValueChanged.AddListener(delegate{OnIdiomaChange();});
		musicVolumeSlider.onValueChanged.AddListener(delegate{OnMusicVolumeChange();});
		applyButton.onClick.AddListener(delegate{OnApplyButtonClick();});
		LoadSettings();
	}


	public void OnIdiomaChange(){
		if(IEspañol.isOn){
			GetComponentInChildren<Text>().text=español;
			gameSettings.idioma="español";
		}else if(IIngles.isOn){
			GetComponentInChildren<Text>().text=ingles;
			gameSettings.idioma="ingles";
		}
	}

	public void OnSubtitulosChange(){
		if(Español.isOn){
			gameSettings.subtitulos = "español";
		}else if(Ingles.isOn){
			gameSettings.subtitulos = "ingles";
		}else if(Apagados.isOn){
			gameSettings.subtitulos = "apagados";
		}
	}

	public void OnMusicVolumeChange(){
		gameSettings.volumen = musicVolumeSlider.value;

	}

	public void OnApplyButtonClick(){
		OnIdiomaChange();
		OnSubtitulosChange();
		SaveSettings();
	}

	public void SaveSettings(){
		idiomadmin.PutIdioma(gameSettings.idioma);
		string jsonData = JsonUtility.ToJson(gameSettings,true);
		File.WriteAllText(Application.persistentDataPath + "/gamesettings.json",jsonData); 
    	SceneManager.LoadScene("MenuPrincipal");
	}

	public void LoadSettings(){
		gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
		if(gameSettings.subtitulos=="ingles"){
			Ingles.isOn=true;
		}else if(gameSettings.subtitulos=="español"){
			Español.isOn=true;
		}else if(gameSettings.subtitulos=="apagados"){
			Apagados.isOn=true;
		}

		if(gameSettings.idioma=="español"){
			IEspañol.isOn=true;
		}else if(gameSettings.idioma=="ingles"){
			IIngles.isOn=true;
		}
		OnIdiomaChange();
		musicVolumeSlider.value = gameSettings.volumen;
	}
}
