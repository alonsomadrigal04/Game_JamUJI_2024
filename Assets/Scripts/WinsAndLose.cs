using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public Player_behaviou player_shit;
    public Rey_behaviour rey_shit;


    private void Awake()
    {
        granny.transform.position = initialPositionGranny;
        wins.transform.position = initialPositionWins;
        king.transform.position = initialPositionKing;

        player_shit = FindObjectOfType<Player_behaviou>();
        rey_shit= FindObjectOfType<Rey_behaviour>();

        esc.DOFade(0f, 0.1f);
        enter.DOFade(0f, 0.1f);
    }
    void Update()
    {
        if(!player_shit.isAlive)
        {
            GrannyWins();
        }
        else if(rey_shit.theKingIsDead)
        {
            KingWins();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            granny.transform.position = initialPositionGranny;
            wins.transform.position = initialPositionWins;
            king.transform.position = initialPositionKing;
        }
    }

    private void GrannyWins()
    {
        granny.transform.DOMove(perfectPositionGranny, 1f).SetEase(Ease.InSine).OnComplete(WordWins);
    }

    private void KingWins()
    {
        king.transform.DOMove(perfectPositionKing, 1f).SetEase(Ease.InSine).OnComplete(WordWins);
    }

    private void WordWins()
    {
        wins.transform.DOMove(perfectPositionWins, 1f).SetEase(Ease.InSine).OnComplete(KeyMotion);
    }

    private void KeyMotion()
    {
        esc.DOFade(100f, 1.0f);
        enter.DOFade(100f, 1.0f);
        key.transform.DOMoveY(transform.position.y + 0.2f, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        //0.2f, 1.5f
    }
}
