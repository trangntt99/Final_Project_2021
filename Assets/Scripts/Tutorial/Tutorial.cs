﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject correctForm, canvas;
    [SerializeField] SpriteRenderer[] animals;
    //[SerializeField] Button btnBack;
    [SerializeField] GameObject trailFX;

    private bool moving = false;
    private bool selected = true;

    // Start is called before the first frame update
    void Start()
    {
        SetEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            //animation hand click

            selected = false;
            StartCoroutine(HandTutorial());
        }
        if (moving)
        {
            gameObject.GetComponent<RectTransform>().position = Vector3.MoveTowards(gameObject.GetComponent<RectTransform>().position, new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z), 10 * Time.deltaTime);
            // move trail when hand move
            trailFX.transform.position = gameObject.GetComponent<RectTransform>().position;
            if (Vector2.Distance(gameObject.GetComponent<RectTransform>().position, correctForm.transform.position) < 0.2f)
            {
                moving = false;
                StartCoroutine(EndofTutorial());
            }
        }
    }

    void SetEnabled(bool flag)
    {
        for(int i = 0; i < animals.Length; i++)
        {
            animals[i].GetComponent<BoxCollider2D>().enabled = flag;
        }
        //btnBack.enabled = flag;
    }

    IEnumerator HandTutorial()
    {
        // click 1 lan thi dung cai dong nay
        //gameObject.GetComponent<Animator>().SetTrigger("SingleClick");
        
        //animation hand click
        gameObject.GetComponent<Animator>().SetTrigger("PressHold");
        yield return new WaitForSeconds(1.5f);
        moving = true;
    }

    IEnumerator EndofTutorial()
    {
        //animation hand click
        gameObject.GetComponent<Animator>().SetTrigger("Release");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        SetEnabled(true);
    }
}