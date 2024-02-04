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

    // ------ VELOCITY ------
    public float velocity;

    // ------ WORDS ------
    public GameObject granny;
    private GameObject vs;
    private GameObject king;

    // ------ TIMER ------
    private float timer;
    private float timerTime;

    private void Awake()
    {
        StartPosition(granny, initialPositionGranny);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            StartPosition(granny, initialPositionGranny);

        if (Input.GetKeyDown(KeyCode.Space))
            WordAnimation(granny, perfectPositionGranny);
    }

    private void WordAnimation(GameObject gameObjectWord, Vector2 perfectPositon)
    {
        gameObjectWord.transform.DOMove(perfectPositon, velocity).SetEase(Ease.InExpo);
    }

    private void StartPosition(GameObject gameObject, Vector2 initialPosition)
    {
        gameObject.transform.position = initialPosition;
    }
}
