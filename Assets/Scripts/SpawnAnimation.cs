using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnAnimation : MonoBehaviour
{

    Vector2 fase01 = new Vector2(1.8f, 0.2f);
    Vector2 fase02 = new Vector2(0.8f, 1.2f);
    Vector2 fase03 = new Vector2(1.0f, 1.0f);


    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    BasicAnimation01();
        //}

    }

    private void Awake()
    {
        BasicAnimation01();
    }

    private void BasicAnimation01() 
    {
        transform.DOScale(fase01, 0.2f).OnComplete(BasicAnimation02);
    }
    private void BasicAnimation02()
    {
        transform.DOScale(fase02, 0.2f).OnComplete(BasicAnimation03);
    }
    private void BasicAnimation03()
    {
        transform.DOScale(fase03, 0.2f);
    }
}

