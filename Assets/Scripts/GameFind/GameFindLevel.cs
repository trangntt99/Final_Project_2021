﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFindLevel : MonoBehaviour
{
    [SerializeField] GameObject popUp, gameWinCanvas, getKeyRewardCanvas;
    [SerializeField] int currentObjectNumber = 0;
    [SerializeField] int totalObjectNumber;
    [SerializeField] string nextScene = null;
    //[SerializeField] float timeWait = 1f;

    private SaveLoadFile saveLoadFile;
    private bool finished = false;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        saveLoadFile = gameObject.AddComponent<SaveLoadFile>();
        //Get random popup
        GetRandomPopUpIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObjectNumber == totalObjectNumber)
        {
            if(nextScene != null && nextScene != "TopicsAlimentsScene")
            {
                SaveCurrentScene();
                StartCoroutine(LoadNextScene());
            }
            else
            {
                saveLoadFile.ResetGameFindFood();

                if (!saveLoadFile.CheckCompleteGame("GameFindFood"))
                {
                    saveLoadFile.IncreaseKey();
                    saveLoadFile.CompleteGame("GameFindFood");
                    this.finished = true;
                }
                StartCoroutine(WinGame());
            } 
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.2f);
        GameObject popUpObject = popUp.transform.GetChild(index).gameObject;
        popUpObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1.2f);
        gameWinCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);

        if (finished && getKeyRewardCanvas != null)
        {
            gameWinCanvas.GetComponentInChildren<Animator>().SetTrigger("Disappear");
            getKeyRewardCanvas.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        SceneManager.LoadScene(nextScene);
    }

    public void Count()
    {
        currentObjectNumber++;
    }
    private void SaveCurrentScene()
    {
        saveLoadFile.SaveCurrentSceneFindFood(nextScene);
    }

    private void GetRandomPopUpIndex()
    {
        if (popUp != null)
        {
            index = Random.Range(0, popUp.transform.childCount);
        }
    }
}
