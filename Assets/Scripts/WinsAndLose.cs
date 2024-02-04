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
    public SpriteRenderer background;


    private void Awake()
    {
        granny.transform.position = initialPositionGranny;
        wins.transform.position = initialPositionWins;
        king.transform.position = initialPositionKing;

        esc.DOFade(0.0f, 0.1f);
        enter.DOFade(0.0f, 0.1f);
        background.DOFade(0.0f, 0.1f);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GrannyWins();
        }
        if(Input.GetKeyDown(KeyCode.K))
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
        background.DOFade(0.6f, 2.0f).SetEase(Ease.OutCubic);
        granny.transform.DOMove(perfectPositionGranny, 1).SetEase(Ease.InSine).OnComplete(WordWins);
    }

    private void KingWins()
    {
        background.DOFade(0.6f, 2.0f).SetEase(Ease.OutCubic);
        king.transform.DOMove(perfectPositionKing, 1).SetEase(Ease.InSine).OnComplete(WordWins);
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
