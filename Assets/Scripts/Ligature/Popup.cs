﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    [SerializeField] string nextScene;

    public string NextScene { get => nextScene; set => nextScene = value; }

    // Start is called before the first frame update
    void Start()
    {
        //word.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (word.GetComponent<Word>().AudioIsStop())
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        SceneManager.LoadScene(nextScene);
        //    }
        //}
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
