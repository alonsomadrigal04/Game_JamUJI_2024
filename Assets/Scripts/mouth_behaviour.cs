using DG.Tweening;
using JoanRuiz.Mipolla;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColiderBocaScript : MonoBehaviour
{
    public float animator_timer = 0;

    public Rey_behaviour reyBehaviour;
    public Animator animatoring;
    SimpleFlash simpleFlash;

    private void Start()
    {
        reyBehaviour = GetComponentInParent<Rey_behaviour>();
        simpleFlash = GetComponentInParent<SimpleFlash>();

    }

    private void Update()
    {
        //animatoring.SetBool("isEated", eated);
        reyBehaviour.animator.SetBool("isEating", reyBehaviour.eated);
        animatoring.SetBool("isEated", reyBehaviour.eated);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Obtén el tag del objeto
        string foodTag = other.tag;

        reyBehaviour.food_list.Add(foodTag);

        if (reyBehaviour != null)
        {
            reyBehaviour.kingEated++;
            CameraShakeManager.instance.Screenshake(0.2f);
            simpleFlash.Flash();
            Debug.Log("he llegado aqui");

            if (foodTag == "Small")
            {
                reyBehaviour.small_cnt++;
            }
            else if (foodTag == "Medium")
            {
                reyBehaviour.medion_cnt++;
            }
            else if (foodTag == "Large")
            {
                reyBehaviour.large_cnt++;
            }

            // Destruye el objeto original
            reyBehaviour.eated = true;
            Destroy(other.gameObject);
        }
    }
}
