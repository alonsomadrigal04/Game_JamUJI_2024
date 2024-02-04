using DG.Tweening;
using JoanRuiz.Mipolla;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class mouth_behaviour : MonoBehaviour
{
    public float animator_timer = 0;

    public Rey_behaviour reyBehaviour;
    public Animator animatoring;
    SimpleFlash simpleFlash;
    public float timer_shit;

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

        if(reyBehaviour.eated == true)
        {
            timer_shit += Time.deltaTime;
        }

        if(timer_shit >= 0.4f)
        {
            timer_shit = 0;
            reyBehaviour.eated = false;
        }
    }

    void TragarAlways()
    {
        animatoring.SetBool("isEated", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Obtén el tag del objeto
        string foodTag = other.tag;

        reyBehaviour.food_list.Add(foodTag);

        if (reyBehaviour != null)
        {
            reyBehaviour.kingEated++;
            CameraShakeManager.instance.Screenshake(0.4f);
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
