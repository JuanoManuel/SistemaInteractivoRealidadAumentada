using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewButton : MonoBehaviour
{
    public Text myText;
    public CanvasManager scrollViewController;

    public void OnClick()
    {
        scrollViewController.UpdateAssetToPlace(myText.text);
    }
}
