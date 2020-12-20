using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IdiomaAdmin : MonoBehaviour
{
    public GameSettings gameSettings;
    public static bool creado=false;


    void Awake() {
        if (!creado)
        {
            DontDestroyOnLoad(this.gameObject);
            creado = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
        try
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        }catch(FileNotFoundException)
        {
            gameSettings = new GameSettings();
            string jsonData = JsonUtility.ToJson(gameSettings, true);
            File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        }
    }

    public string SelecIdioma()
    {
        return gameSettings.idioma;
    }

    public void PutIdioma(string idioma)
    {
        gameSettings.idioma = idioma;
    }
}
