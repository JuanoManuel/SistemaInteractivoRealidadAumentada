using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaManager : MonoBehaviour
{
    public string galleryName;
    public RawImage photoTexture;
    public Canvas[] canvasToHideInPhoto;
    public Image mediaWindow;
    public CanvasManager gameManager;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TakePhoto()
    {
        StartCoroutine(CaptureImageFromScreen(galleryName));
        
    }

    public IEnumerator CaptureImageFromScreen(string galleryName)
    {
        gameManager.PauseInteractive();
        foreach (Canvas canvas in canvasToHideInPhoto)
            canvas.enabled = false;

        yield return new WaitForEndOfFrame();

        //create a new texture
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //reading pixels of the screen
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height),0,0);
        screenshot.Apply();
        photoTexture.texture = screenshot;
        string fileName = "ARMuseum-" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")+".png";
        //saving the photo
        NativeGallery.Permission permission =  NativeGallery.SaveImageToGallery(screenshot, galleryName, fileName, (success , path) => Debug.Log("Media result: "+success+" "+path));

        Debug.Log("Permission: " + permission);
        
        foreach (Canvas canvas in canvasToHideInPhoto)
            canvas.enabled = true;
        audioSource.Stop();
        audioSource.Play();
        yield return new WaitForSeconds(1);
        mediaWindow.GetComponent<Animator>().SetBool("IsActive", true);
    }

    public void CloseMediaWindow()
    {
        mediaWindow.GetComponent<Animator>().SetBool("IsActive", false);
        gameManager.ResumeInteractive();
    }
}
