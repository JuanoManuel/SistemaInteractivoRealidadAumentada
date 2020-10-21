using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CanvasManager : MonoBehaviour
{
    public Animator menuPanelAnimator, trackingCanvasAnimator, startDialogAnimator;
    public TapToPlace tapToPlace;
    public GameObject[] prefabsToPlace;
    public Text subtitlesText;
    public GameObject currentPrefab;

    private Dictionary<string, GameObject> content = new Dictionary<string, GameObject>();
    private ARPlaneManager planeManager;
    private bool isRunningExplication, isReadyToPlay;

    private void Awake()
    {
        foreach(GameObject prefab in prefabsToPlace)
        {
            content.Add(prefab.name, prefab);
        }
        planeManager = GetComponent<ARPlaneManager>();
    }

    public void UpdateAssetToPlace(string key)
    {
        currentPrefab = content[key];
        tapToPlace.SetSpawnObjectToNull();
        tapToPlace.SetPrefab(currentPrefab);
        menuPanelAnimator.SetBool("IsActive", false);
        trackingCanvasAnimator.SetBool("IsActive", true);
    }

    public void SetReadyToPlay(bool value)
    {
        isReadyToPlay = value;
        if (value)
        {
            startDialogAnimator.SetBool("IsActive", true);
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
            currentPrefab.GetComponent<SyncExplication>().start = true;
            startDialogAnimator.SetBool("IsActive", false);
            trackingCanvasAnimator.SetBool("IsActive", true);
        }
        else
        {
            Debug.Log("No hay nada que mostrar");
        }
        
    }

    public void FinishInteractive()
    {
        SetAllPlanesActive(true);
        menuPanelAnimator.SetBool("IsActive", true);
        trackingCanvasAnimator.SetBool("IsActive", false);
        isRunningExplication = false;
        isReadyToPlay = false;
    }

    public Text GetSubtitlesText()
    {
        return subtitlesText;
    }

    private void SetAllPlanesActive(bool value)
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
