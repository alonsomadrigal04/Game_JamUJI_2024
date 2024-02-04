using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinsAndLose : MonoBehaviour
{
    private Vector2 perfectPositionGranny = new Vector2(-0.97f, 0.61f);
    private Vector2 perfectPositionWins = new Vector2(0.99f, 0.61f);
    private Vector2 perfectPositionKing = new Vector2(-0.6f, 0.61f);

    private Vector2 initialPositionGranny = new Vector2(-3.68f, 0.61f);
    private Vector2 initialPositionWins = new Vector2(3.37f, 0.61f);
    private Vector2 initialPositionKing = new Vector2(-3.68f, 0.61f);

    public GameObject granny;
    public GameObject wins;
    public GameObject king;

    public GameObject key;
    public SpriteRenderer esc;
    public SpriteRenderer enter;
    public SpriteRenderer background;

    public Player_behaviou player_shit;
    public Rey_behaviour king_shit;

    public bool hasanimated=false;

    public AudioClip king_winsSound;
    public AudioClip granny_wins;
    public bool hassounded;
    public float timer_sound;
    public ParticleSystem confeti;

    public float win_timer;
    public bool win_bool;


    public bool sounplayed;

    public bool otroboolmas;


    private void Awake()
    {
        granny.transform.position = initialPositionGranny;
        wins.transform.position = initialPositionWins;
        king.transform.position = initialPositionKing;
        

        player_shit = FindObjectOfType<Player_behaviou>();
        king_shit = FindObjectOfType<Rey_behaviour>();

        esc.DOFade(0.0f, 0.1f);
        enter.DOFade(0.0f, 0.1f);
        background.DOFade(0.0f, 0.1f);
        confeti.Stop();
    }
    void Update()
    {
        if(!player_shit.isAlive && !hasanimated)
        {
            KingWins();
            hasanimated = true;
            
        }
        if(king_shit.theKingIsDead && !hasanimated)
        {
            //GrannyWins();
            win_bool= true;
            hasanimated = true;
        }
        if(hasanimated)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string nombreEscena = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(nombreEscena);


            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            granny.transform.position = initialPositionGranny;
            wins.transform.position = initialPositionWins;
            king.transform.position = initialPositionKing;
        }

        if(win_bool)
        {
            win_timer += Time.deltaTime;
        }
        if(win_timer >= 10.0f && !otroboolmas)
        {
            GrannyWins();
            otroboolmas = true;
            AudioSource.PlayClipAtPoint(granny_wins, transform.position);
        }

        if(hassounded)
        {
            timer_sound += Time.deltaTime;
        }

        if(timer_sound >= 0.7f && !sounplayed)
        {
            AudioSource.PlayClipAtPoint(king_winsSound, transform.position);
            sounplayed = true;
        }


    }

    private void GrannyWins()
    {
        background.DOFade(0.6f, 2.0f).SetEase(Ease.OutCubic);
        granny.transform.DOMove(perfectPositionGranny, 1).SetEase(Ease.InSine).OnComplete(WordWins);
        confeti.Play();
    }

    public void KingWins()
    {
        background.DOFade(0.6f, 2.0f).SetEase(Ease.OutCubic);
        king.transform.DOMove(perfectPositionKing, 1).SetEase(Ease.InSine).OnComplete(WordWins);
        
        hassounded = true;
    }

    private void WordWins()
    {
        wins.transform.DOMove(perfectPositionWins, 1).SetEase(Ease.InSine).OnComplete(KeyMotion);
    }

    private void KeyMotion()
    {
        esc.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic);
        enter.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic);
        key.transform.DOMoveY(transform.position.y + 0.2f, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        //0.2f, 1.5f
    }
}
