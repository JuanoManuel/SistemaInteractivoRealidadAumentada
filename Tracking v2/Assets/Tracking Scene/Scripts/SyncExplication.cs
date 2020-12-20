using System;
using System.Collections.Generic;
using UnityEngine;

public class SyncExplication : MonoBehaviour
{
    [Header("Initial Settings")]
    //banderas para reproducir audio y mostrar subtitulos
    public bool playAudio;
    public bool displaySubtitles;
    //indica si es el ultimo modelo de la explicacion
    public bool isLast;
    //Objetos que animaremos
    [Header("Animation Settings")]
    public Animator[] objectsToAnimate;
    //Audios que se reproduciran, el nombre de los audios es importante
    [Header("Audio Settings")]
    public AudioClip[] audiosToPlay;
    //Nombre de los capitulos de acuerdo a los json
    [Header("Json Settings")]
    //nombre del archivo json
    public string fileName = "string";
    public string[] topicsToPlay;
    [Space]
    //variable que cuenta el tiempo transcurrido
    public float time;
    //bandera para iniciar el timer
    public bool start;
    //bandera para saber si se esta reproduciend un audio
    private bool playingAudio;
    //diccionario de objetos por animar
    private Dictionary<string, Animator> objects = new Dictionary<string, Animator>();
    //lastIndex controla el ultimo subtitulo reproducido conforme el orden del json
    //topicIndex lo mismo pero con los topics
    private int lastIndex, topicIndex;
    //Diccionario de Objetos Subtitulo aqui se guarda el texto y el tiempo para reproducir
    private Dictionary<string, Subtitle[]> subtitles = new Dictionary<string, Subtitle[]>();
    //tema actual de la explicacion
    private string currentTopic;
    //idioma en el que se mostraran los subtitulos
    //en: english es:español
    private string idioma = "es";
    //audioSource de la camara principal
    private AudioSource audioSource;
    //referencia al gamecontroller
    private CanvasManager gameController;

    private void Awake()
    {
        //referencia al gamecontroller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CanvasManager>();
        //referencia al control de audio de la camara
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }
    private void Start()
    {
        time = 0f;
        lastIndex = 0;
        playingAudio = true;
        currentTopic = topicsToPlay[0];
        topicIndex = 0;
    }

    private void OnEnable()
    {
        //obtenemos el idioma en el que se mostraran los subtitulos
        displaySubtitles = true;
        if (gameController.GetSubtitles() == "español")
            idioma = "es";//terminacion "es" para español
        else if (gameController.GetSubtitles() == "ingles")
            idioma = "en";//terminacion "en" para ingles
        else
            displaySubtitles = false;
        Debug.Log(idioma);
        //Cargamos el Json a sus Objetos equivalentes
        var textAsset = Resources.Load<TextAsset>("Subtitles/" + fileName + "-" + idioma);
        var voText = JsonUtility.FromJson<SubtitlesPack>(textAsset.text);
        //Poblamos el diccionario de subtitulos
        foreach (var t in voText.pack)
        {
            subtitles[t.name] = t.subtitles;
        }
        //poblamos el diccionario de objetos que animar
        foreach (var obj in objectsToAnimate)
        {
            objects[obj.gameObject.name] = obj;
        }
    }

    private void Update()
    {
        if (start)
        {
            //si se quiere reproducir audio cargamos los audios
            if (playAudio)
            {
                //Buscamos en cada clip el clip que termine con el topic actual para reproducirlo
                foreach (AudioClip clip in audiosToPlay)
                {
                    if (fileName + "-" + currentTopic == clip.name && playingAudio)
                    {
                        audioSource.PlayOneShot(clip);
                        playingAudio = false;
                    }
                }
            }
            //Mostramos subtitulos correspondientes usando la variable time

            //obtenemos todos los subtitulos del tema actual
            Subtitle[] pack = subtitles[currentTopic];
            //si aun no llegamos al ultimo subtitulo
            if (lastIndex < pack.Length)
            {
                //si el tiempo actual es igual o mayor al tiempo requerido para mostrar el subtitulo en el que 
                //vamos entonces si se muestra
                if (time >= Double.Parse(pack[lastIndex].time))
                {
                    //actualizamos el texto en los subtitulos
                    if (displaySubtitles && pack[lastIndex].subtitle != null)
                        gameController.GetSubtitlesText().text = pack[lastIndex].subtitle;
                    //almacenamos el nombre del objeto a animar
                    string trigger = pack[lastIndex].trigger;
                    //si el objeto tiene una propiedad trigger iniciamos la animacion correspondiente
                    if (trigger != string.Empty)
                    {
                        bool nextValue = !objects[trigger].GetBool("IsActive");
                        objects[trigger].SetBool("IsActive", nextValue);
                        if (objects[trigger].TryGetComponent(out PinManager pinManager))
                        {
                            pinManager.value = nextValue;
                            pinManager.start = true;
                        }
                    }
                    lastIndex++;
                }
                time += Time.deltaTime;
            }
            else
            {
                //esperamos a que se termine de reproducir el audio actual
                if (!audioSource.isPlaying)
                {
                    topicIndex++;
                    //Si se supero el numero de topics la explicacion acabo
                    //Reseteamos todos los canvas del gameManager
                    if (topicIndex >= topicsToPlay.Length)
                    {
                        start = false;
                        topicIndex = 0;
                        if (displaySubtitles)
                            gameController.GetSubtitlesText().text = string.Empty;
                        //si es el ultimo se manda al gamecontroller que termino la explicacion
                        //si no se empieza con la explicacion del siguiente modelo
                        if (isLast)
                        {
                            gameController.FinishInteractive();
                        }
                    }
                    else
                    {
                        //Si no entonces preparamos todo para iniciar con el siguente tema
                        currentTopic = topicsToPlay[topicIndex];
                        playingAudio = true;
                        lastIndex = 0;
                        time = 0;
                    }
                }
            }
        }
    }

    public void StartTimer()
    {
        start = true;
        if(playAudio)
            audioSource.UnPause();
    }

    public void StopTimer()
    {
        start = false;
        if(playAudio)
            audioSource.Pause();
    }
}
