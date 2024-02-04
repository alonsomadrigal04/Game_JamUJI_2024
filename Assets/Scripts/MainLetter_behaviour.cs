using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.U2D;

public class MainLetter_behaviour : MonoBehaviour
{
    // ------ INITIAL POSITON ------
    private Vector2 initialPositionGranny = new Vector2(-1.262021f, 2.25f);
    private Vector2 initialPositionVs = new Vector2(0.4054899f, 2.25f);
    private Vector2 initialPositionKing = new Vector2(1.758207f, 2.25f);

    // ------ PERFECT POSITON ------
    private Vector2 perfectPositionGranny = new Vector2 (-1.262021f, -0.03985758f);
    private Vector2 perfectPositionVs = new Vector2(0.4054899f, 0.0040671f);
    private Vector2 perfectPositionKing = new Vector2(1.758207f, -0.08093529f);

    // ------ TIEMPO ------
    public float tiempo;
    public float titleTiempo;
    public bool hasReproduced;

    // ------ WORDS ------
    public GameObject granny;
    public GameObject vs;
    public GameObject king;

    public SpriteRenderer grannyFade;
    public SpriteRenderer vsFade;
    public SpriteRenderer kingFade;

    public SpriteRenderer shine;

    // ------ TIMER ------
    public float timer;
    private float timerTime = 1;
    private bool timerCheck = false;

    // ------ TIMER ANIMATOR ------
    public float timerAnimator;
    private float timerTimeAnimator = 1;
    private bool timerCheckAnimator = false;

    // ------ TIMER ANIMATOR ------
    Player_behaviou player_shit;

    private bool checkerFade = false;

    public AudioClip Title_sound;
    public AudioClip main_Theme;
    public AudioClip Shine;



    public int contador = 0;

    Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        StartPosition(granny, initialPositionGranny);
        StartPosition(vs, initialPositionVs);
        StartPosition(king, initialPositionKing);
        player_shit = FindObjectOfType<Player_behaviou>();
    }


    void Update()
    {
        MainAnimation();
        if(timerCheck)
        {
            timer += Time.deltaTime;
        }

        if (timerCheckAnimator)
        {
            timerAnimator += Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    StartPosition(granny, initialPositionGranny);
        //    StartPosition(vs, initialPositionVs);
        //    StartPosition(king, initialPositionKing);
        //    contador = 0;
        //}

        titleTiempo += Time.deltaTime;

        CheckTempo();
    }
    private void PlayRandomEatingSound()
    {
        AudioSource.PlayClipAtPoint(Shine, transform.position);
        AudioSource.PlayClipAtPoint(main_Theme, transform.position);
    }
    public void CheckTempo()
    {
        if(titleTiempo >= 1.2f)
        {
            if (!hasReproduced)
            {
                AudioSource.PlayClipAtPoint(Title_sound, transform.position);
                hasReproduced = true;
                
            }
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

    private void TimerAnimator()
    {
        timerCheckAnimator = true;
        if (timerAnimator > timerTimeAnimator)
        {
            timerAnimator = 0;
            timerCheck = false;
            shine.DOFade(100f, 0.1f);
            animator.SetBool("Shine", true);
        }
    }

    private void MainAnimation()
    {
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
            king.transform.DOMove(perfectPositionKing, tiempo).SetEase(Ease.InSine).OnComplete(TimerAnimator);
            Timer();
        }
    }
    private void EndShineAnimation()
    {
        shine.DOFade(0f, 0.1f).OnComplete(CheckerFade);
        player_shit.canMove = true;
    }

    private void CheckerFade()
    {
        Debug.Log("EstoyInside");
        grannyFade.DOFade(0f, 2f).SetEase(Ease.OutCubic);
        vsFade.DOFade(0f, 2f).SetEase(Ease.OutCubic);
        kingFade.DOFade(0f, 2f).SetEase(Ease.OutCubic);
        
    }
}
