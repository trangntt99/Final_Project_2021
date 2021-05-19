﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish;
    private float startPosX;
    private float startPosY;
    private Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (finish == false)
        {
            if (moving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                this.gameObject.transform.position = new Vector3(mousePos.x - startPosX,
                                                                    mousePos.y - startPosY,
                                                                    this.gameObject.transform.position.z);
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;

        if (Mathf.Abs(this.transform.position.x - correctForm.transform.position.x) <= 0.5f &&
            Mathf.Abs(this.transform.position.y - correctForm.transform.position.y) <= 0.5f)
        {
            this.transform.position    = new Vector3(correctForm.transform.position.x,
                                                       correctForm.transform.position.y,
                                                       correctForm.transform.position.z - 1); // not overridden by image shadow
            this.transform.localScale       = new Vector3(correctForm.transform.localScale.x,
                                                       correctForm.transform.localScale.y,
                                                       correctForm.transform.localScale.z);
            finish = true;

            GameObject.Find("PointsHandle").GetComponent<WinScript>().AddPoints();
        }
        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }
}
