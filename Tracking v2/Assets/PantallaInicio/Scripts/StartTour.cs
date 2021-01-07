using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartTour : MonoBehaviour
{
    public GameObject videoObject;
    public Animator botonSkip;
    public VideoClip videoTeotihuacan, videoMexicas;
    public GameObject avisoCargando;

    GameObject controller;
    VideoPlayer videoPlayer;
    Animator videoAnimator;
    RawImage rawImageVideo;
    bool flagCultura; //true: Teotihuacan false: Mexicas
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Start()
    {
        videoPlayer = videoObject.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += VideoEnded;
        videoAnimator = videoObject.GetComponent<Animator>();
        rawImageVideo = videoObject.GetComponent<RawImage>();
    }

    public void OpenTeotihuacan()
    {
        OpenScene(true, videoTeotihuacan);
    }

    public void OpenMexicas()
    {
        OpenScene(false, videoMexicas);
    }

    public void OpenMainMenu()
    {
        IdiomaAdmin.creado = false;
        Destroy(controller);
        SceneManager.LoadScene("MenuPrincipal");
    }

    void VideoEnded(VideoPlayer player)
    {
        avisoCargando.GetComponent<Animator>().SetBool("Enable", true);
    }

    public void VideoEnded()
    {
        avisoCargando.GetComponent<Animator>().SetBool("Enable", true);
    }

    void OpenScene(bool flagValue, VideoClip clip)
    {
        flagCultura = flagValue;
        avisoCargando.GetComponent<ShowLoadingMessage>().flagCultura = flagCultura;
        videoAnimator.SetBool("IsActive", true);
        rawImageVideo.raycastTarget = true;
        botonSkip.SetBool("Enable", true);
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }
}
