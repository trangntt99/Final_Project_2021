﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NumberActiver : MonoBehaviour
{
    //public TextWriter textWriter;
    public GameObject[] numbers;
    public GameObject textBackground;
    public GameObject[] textsInTextBackground;

    private Text message;
    private SaveLoadFile slf;

    // Start is called before the first frame update
    void Start()
    {
        slf = gameObject.AddComponent<SaveLoadFile>();
        StartCoroutine(printNumberCoroutine());
    }

    IEnumerator printNumberCoroutine()
    {
        yield return new WaitForSeconds(1f);
        textBackground.SetActive(true);
        for(int i = 0; i < textsInTextBackground.Length-1; i++)
        {
            textsInTextBackground[i].SetActive(true);
            yield return new WaitForSeconds(4f);
            textsInTextBackground[i].SetActive(false);
        }

        //yield return new WaitForSeconds(2f);
        //textWriter.addWriter(message, "Aujourd'hui on va apprendre les chiffres 0-10", .1f);
        //yield return new WaitForSeconds(5f);
        //textWriter.addWriter(message, "Alors, comptez avec moi", .1f);
        //yield return new WaitForSeconds(3f);
        textBackground.SetActive(false);

        foreach (GameObject num in numbers)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2.5f);
            num.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        textsInTextBackground[textsInTextBackground.Length - 1].SetActive(true);
        textBackground.SetActive(true);

        if (!slf.CheckCompleteGame("NumberIntroduce"))
        {
            slf.CompleteGame("NumberIntroduce");
        }
    }
}
