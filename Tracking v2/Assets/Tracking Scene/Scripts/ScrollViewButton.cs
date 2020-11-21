using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewButton : MonoBehaviour
{
    public CanvasManager scrollViewController;
    public string prefabName;

    public void OnClick()
    {
        scrollViewController.UpdateAssetToPlace(prefabName);
    }
}
