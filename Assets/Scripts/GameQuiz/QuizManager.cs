﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private QuizDataScriptable quizData;
    [SerializeField] private TMP_Text countScene;

    private List<Question> questions;
    private Question selectedQuestion;
    private int index;
    private SaveLoadFile slf;
    private bool finished = false;

    public bool Finished { get => finished; set => finished = value; }

    // Start is called before the first frame update
    void Start()
    {
        slf = gameObject.AddComponent<SaveLoadFile>();
        slf.QuizData = quizData;
        List<Question> list = slf.LoadCurrentList();
        Question q = slf.LoadCurrentQuestion();

        if (list == null)
        {
            questions = new List<Question>(slf.QuizData.questions);
        }
        else
        {
            questions = list;
        }
        if(q == null)
        {
            SelectQuestion();
        }
        else
        {
            selectedQuestion = q;
            quizUI.SetQuestion(selectedQuestion);
            this.index = questions.IndexOf(q);
        }

        countScene.text = (slf.QuizData.questions.Count + 1 - questions.Count).ToString();
    }

    void SelectQuestion()
    {
        if (questions.Count <= 0)
        {
            //reset game quiz
            slf.ResetCurrentQuestion_GameQuiz();
            slf.ResetCurrentList_GameQuiz();

            //StartCoroutine(NextRound(8f));
            //SceneManager.LoadScene("TopicsAnimalsScene");
        }
        else
        {
            int val = Random.Range(0, questions.Count);
            selectedQuestion = questions[val];

            //save game quiz
            slf.SaveCurrentQuestion(selectedQuestion);

            quizUI.SetQuestion(selectedQuestion);
            this.index = val;
            countScene.text = (slf.QuizData.questions.Count + 1 - questions.Count).ToString();
        }

        quizUI.SetEnabled(true);
    }

    public int Answer(string answered)
    {
        int correctAns = 0;
        float delayTime = 0;
        if (answered == selectedQuestion.correctAns && questions.Count > 1)
        {
            //YES
            correctAns = 1;
            questions.RemoveAt(this.index);
            slf.ResetCurrentQuestion_GameQuiz();
            slf.SaveCurrentList(questions);
            delayTime = 3f;
        }
        else if(answered == selectedQuestion.correctAns && questions.Count <= 1)
        {
            correctAns = -1;
            questions.RemoveAt(this.index);
            slf.ResetCurrentQuestion_GameQuiz();
            slf.SaveCurrentList(questions);
            delayTime = 3f;

            //win game
            if (!slf.CheckCompleteGame("GameQuiz"))
            {
                //inscrease key
                slf.IncreaseKey();

                //complete game
                slf.CompleteGame("GameQuiz");

                this.finished = true;
            }
        }
        else
        {
            correctAns = 0;
            delayTime = 1f;
        }

        Invoke("SelectQuestion", delayTime);

        return correctAns;
    }

    /*IEnumerator NextRound(float delayTime)
    {
        //win game

        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("TopicsAnimalsScene");
    }*/
}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public Sprite questionImg;
    public AudioClip audioQuestion;
    public List<Answer> options;
    public string correctAns;
}

[System.Serializable]
public class Answer
{
    public string answerText;
    public Sprite answerSprite;
    public AudioClip answerClip;
}

[System.Serializable]
public class AnswerUI
{
    public TMP_Text answerText;
    public Image answerImg;
    public Image speakImg;
}