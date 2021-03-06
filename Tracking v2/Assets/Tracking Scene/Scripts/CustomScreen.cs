﻿using UnityEngine;

public class CustomScreen : MonoBehaviour
{
    CanvasGroup canvasGroup;

    protected virtual void Start()
    {
        print("Si me usan");
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected void SetScreen(bool open)
    {
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
        canvasGroup.alpha = open ? 1 : 0;
    }
}
