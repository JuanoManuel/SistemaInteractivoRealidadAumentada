using System;
using System.Collections.Generic;
using UnityEngine;

public class SyncExplication : MonoBehaviour
{
    public Animator[] objectsToAnimate;
    public AudioClip[] audiosToPlay;
    public string[] topicsToPlay;
    public bool playAudio;
    public float time;
    public string fileName = "string";

    private bool start;
    private Dictionary<string, Animator> objects = new Dictionary<string, Animator>();
    private int lastIndex, topicIndex;
    private Dictionary<string, Subtitle[]> subtitles = new Dictionary<string, Subtitle[]>();
    private string currentTopic;
    private AudioSource audioSource;
    private CanvasManager gameController;

    private void Awake()
    {
        //load the dialogs from file
        var textAsset = Resources.Load<TextAsset>("Subtitles/"+fileName);
        var voText = JsonUtility.FromJson<SubtitlesPack>(textAsset.text);
        foreach(var t in voText.pack)
        {
            subtitles[t.name] = t.subtitles;
        }
        foreach(var obj in objectsToAnimate)
        {
            objects[obj.gameObject.name] = obj;
        }
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CanvasManager>();
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

    }
    private void Start()
    {
        time = 0f;
        lastIndex = 0;
        start = false;
        playAudio = true;
        currentTopic = topicsToPlay[0];
        topicIndex = 0;
    }

    private void Update()
    {
        if (start)
        {
            foreach(AudioClip clip in audiosToPlay)
            {
                if (fileName + "-" + currentTopic == clip.name && playAudio)
                {
                    audioSource.PlayOneShot(clip);
                    playAudio = false;
                }
            }
            Subtitle[] pack = subtitles[currentTopic];
            if (lastIndex < pack.Length)
            {
                if (time >= Double.Parse(pack[lastIndex].time))
                {
                    gameController.GetSubtitlesText().text = pack[lastIndex].subtitle;
                    string trigger = pack[lastIndex].trigger;
                    if (trigger != string.Empty)
                    {
                        objects[trigger].SetBool("IsActive", true);
                        if(objects[trigger].gameObject.tag == "MoreInfo")
                            objects[trigger].gameObject.GetComponent<PinManager>().start = true;
                    }
                    lastIndex++;
                }
                time += Time.deltaTime;
            }
            else
            {
                Debug.Log("Fin... Esperando a que se termine de reproducir el audio");
                if (!audioSource.isPlaying)
                {
                    topicIndex++;
                    if (topicIndex >= topicsToPlay.Length)
                    {
                        Debug.Log("Termino explicacion");
                        start = false;
                        topicIndex = 0;
                        gameController.GetSubtitlesText().text = string.Empty;
                        gameController.FinishInteractive();
                    }
                    else
                    {
                        currentTopic = topicsToPlay[topicIndex];
                        playAudio = true;
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
        audioSource.UnPause();
    }

    public void StopTimer()
    {
        start = false;
        audioSource.Pause();
    }
}
