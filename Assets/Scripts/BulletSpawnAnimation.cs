using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnAnimation : MonoBehaviour
{
    Vector2 fase01 = new Vector2(0.92f, 1.8f);
    Vector2 fase02 = new Vector2(1.5f, 0.8f);
    Vector2 fase03 = new Vector2(1.0f, 1.0f);


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            BasicAnimationBullet01();
        }

    }

    private void Awake()
    {
        //BasicAnimationBullet01();
    }

    private void BasicAnimationBullet01()
    {
        transform.DOScale(fase01, 0.2f).OnComplete(BasicAnimationBullet02);
    }
    private void BasicAnimationBullet02()
    {
        transform.DOScale(fase02, 0.2f).OnComplete(BasicAnimationBullet03);
    }
    private void BasicAnimationBullet03()
    {
        transform.DOScale(fase03, 0.2f);
    }
}
