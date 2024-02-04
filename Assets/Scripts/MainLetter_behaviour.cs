using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.U2D;

public class MainLetter_behaviour : MonoBehaviour
{
    // ------ INITIAL POSITON ------
    private Vector2 initialPositionGranny = new Vector2(-0.9283695f, 2.25f);
    private Vector2 initialPositionVs = new Vector2(0.4286305f, 2.25f);
    private Vector2 initialPositionKing = new Vector2(1.50763f, 2.25f);

    // ------ PERFECT POSITON ------
    private Vector2 perfectPositionGranny = new Vector2 (-0.9283695f, 0.03942397f);
    private Vector2 perfectPositionVs = new Vector2(0.4286305f, 0.07242402f);
    private Vector2 perfectPositionKing = new Vector2(1.50763f, 0.009423941f);

    // ------ TIEMPO ------
    public float tiempo;

    // ------ WORDS ------
    public GameObject granny;
    public GameObject vs;
    public GameObject king;

    // ------ TIMER ------
    public float timer;
    private float timerTime = 1;
    private bool timerCheck = false;

    public int contador = 0;

    private void Awake()
    {
        StartPosition(granny, initialPositionGranny);
        StartPosition(vs, initialPositionVs);
        StartPosition(king, initialPositionKing);
    }
    void Update()
    {
        if(timerCheck)
        {
            timer += Time.deltaTime;
        }

        if (contador == 0)
        {
            WordAnimation(granny, perfectPositionGranny);
            Timer();
        }
        else if (contador == 1)
        {
            WordAnimation(vs, perfectPositionVs);
            Timer();
        }
        else if (contador == 2)
        {
            WordAnimation(king, perfectPositionKing);
            Timer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartPosition(granny, initialPositionGranny);
            StartPosition(vs, initialPositionVs);
            StartPosition(king, initialPositionKing);
            contador = 0;
        }
    }

    private void WordAnimation(GameObject gameObjectWord, Vector2 perfectPositon)
    {
        gameObjectWord.transform.DOMove(perfectPositon, tiempo).SetEase(Ease.InSine);
    }

    private void StartPosition(GameObject gameObject, Vector2 initialPosition)
    {
        gameObject.transform.position = initialPosition;
    }

    private void Timer()
    {
        timerCheck = true;
        if(timer > timerTime)
        {
            timer = 0;
            timerCheck = false;
            contador++;
        }
    }
}
