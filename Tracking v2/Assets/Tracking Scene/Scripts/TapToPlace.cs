using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public CanvasManager canvasManager;
    private GameObject spawnObject;
    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (!touchPosition.IsPointOverIU() && raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpose = hits[0].pose;

            if(spawnObject == null && prefabToInstantiate != null)
            {
                spawnObject = Instantiate(prefabToInstantiate, hitpose.position, hitpose.rotation);
                canvasManager.currentPrefab = spawnObject;
                canvasManager.SetReadyToPlay(true);
            }
        }
    }

    public void SetSpawnObjectToNull()
    {
        Destroy(spawnObject);
        spawnObject = null;

    }

    public void SetPrefab(GameObject prefab)
    {
        prefabToInstantiate = prefab;
    }
}
