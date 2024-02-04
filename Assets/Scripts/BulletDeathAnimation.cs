using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeathAnimation : MonoBehaviour
{
    Vector2 death = new Vector2(0.0001f, 0.00001f);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DeathAnimation();
        }

    }
    private void DeathAnimation()
    {
        transform.DOScale(death, 0.2f);
    }
}
