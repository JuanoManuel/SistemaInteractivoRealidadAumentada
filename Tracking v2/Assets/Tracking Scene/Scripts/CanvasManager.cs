using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CanvasManager : MonoBehaviour
{
    public Animator menuPanelAnimator, trackingCanvasAnimator, startDialogAnimator,helpPanel;
    public TapToPlace tapToPlace;
    public GameObject[] prefabsToPlace;
    public Text subtitlesText;
    public GameObject currentPrefab;
    public Text textInstruction;

    private Dictionary<string, GameObject> content = new Dictionary<string, GameObject>();
    private ARPlaneManager planeManager;
    private bool isRunningExplication, isReadyToPlay;
    private Dictionary<string, string> instructions = new Dictionary<string, string>();

    private void Awake()
    {
        foreach(GameObject prefab in prefabsToPlace)
        {
            content.Add(prefab.name, prefab);
        }
        instructions.Add("inicio", "Selecciona un tema del menú");
        instructions.Add("colocar", "Toca el plano generado");
        instructions.Add("ajustar", "Ajusta tamaño, posición y rotación");
        instructions.Add("reproduciendo", string.Empty);
        planeManager = GetComponent<ARPlaneManager>();
    }

    private void Start()
    {
        textInstruction.text = instructions["inicio"];
    }

    public void UpdateAssetToPlace(string key)
    {
        currentPrefab = content[key];
        tapToPlace.SetSpawnObjectToNull();
        tapToPlace.SetPrefab(currentPrefab);
        menuPanelAnimator.SetBool("IsActive", false);
        trackingCanvasAnimator.SetBool("IsActive", true);
        textInstruction.text = instructions["colocar"];
    }

    public void SetReadyToPlay(bool value)
    {
        isReadyToPlay = value;
        if (value)
        {
            startDialogAnimator.SetBool("IsActive", true);
            textInstruction.text = instructions["ajustar"];
        }
    }

    public void ToggleCanvas()
    {
        if (!isRunningExplication)
        {
            menuPanelAnimator.SetBool("IsActive", !menuPanelAnimator.GetBool("IsActive"));
            trackingCanvasAnimator.SetBool("IsActive", !trackingCanvasAnimator.GetBool("IsActive"));
            if (isReadyToPlay)
            {
                startDialogAnimator.SetBool("IsActive", !startDialogAnimator.GetBool("IsActive"));
            }
        }
    }

    public void StartInteractive()
    {
        if (currentPrefab != null)
        {
            isRunningExplication = true;
            SetAllPlanesActive(false);
            currentPrefab.GetComponent<SyncExplication>().StartTimer();
            startDialogAnimator.SetBool("IsActive", false);
            trackingCanvasAnimator.SetBool("IsActive", true);
            textInstruction.text = instructions["reproduciendo"];
        }
        else
        {
            Debug.Log("No hay nada que mostrar");
        }
        
    }

    public void PauseInteractive()
    {
        if(isRunningExplication)
            currentPrefab.GetComponent<SyncExplication>().StopTimer();
    }

    public void ResumeInteractive()
    {
        if(isRunningExplication)
            currentPrefab.GetComponent<SyncExplication>().StartTimer();
    }

    public void FinishInteractive()
    {
        SetAllPlanesActive(true);
        menuPanelAnimator.SetBool("IsActive", true);
        trackingCanvasAnimator.SetBool("IsActive", false);
        isRunningExplication = false;
        isReadyToPlay = false;
        textInstruction.text = instructions["inicio"];
    }

    public Text GetSubtitlesText()
    {
        return subtitlesText;
    }

    private void SetAllPlanesActive(bool value)
    {
        planeManager.enabled = value;
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

    public void CloseHelpCanvas()
    {
        if(isRunningExplication)
            ResumeInteractive();
        helpPanel.SetBool("IsActive", false);
    }

    public void OpenHelpCanvas()
    {
        helpPanel.SetBool("IsActive", true);
        if(isRunningExplication)
            PauseInteractive();
    }
}
