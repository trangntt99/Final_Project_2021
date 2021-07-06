﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PuzzleImageManager : MonoBehaviour
{
    public GameObject mainCharac;
    public GameObject flashLight;

    private GameObject message, buttons;

    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("ImageText");
        message.SetActive(false);
        buttons = GameObject.Find("Buttons");
        buttons.SetActive(false);

        StartCoroutine(runAnim());
    }

    IEnumerator runAnim()
    {
        yield return new WaitForSeconds(1f);
        buttons.SetActive(true);
        yield return new WaitForSeconds(1f);
        mainCharac.SetActive(true);
        yield return new WaitForSeconds(3f);
        message.SetActive(true);
    }

    public void TakeScreenShot()
    {
        StartCoroutine(takeAShot());
    }

    IEnumerator takeAShot()
    {
        yield return new WaitForSeconds(.8f);
        buttons.SetActive(false);

        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        string name = "picture" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg";

        NativeGallery.SaveImageToGallery(texture, "Monde Ludique", name);

        flashLight.SetActive(true);

        yield return new WaitForSeconds(.5f);
        buttons.SetActive(true);
        flashLight.SetActive(false);

        Destroy(texture);
    }
}
