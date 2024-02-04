using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColiderBocaScript : MonoBehaviour
{
    public float animator_timer = 0;
    public bool eated;

    public Rey_behaviour reyBehaviour;
    public Animator animatoring;

    private void Start()
    {
        reyBehaviour = GetComponentInParent<Rey_behaviour>();

    }

    private void Update()
    {
        if (eated)
        {
            animator_timer += Time.deltaTime;
            if(animator_timer > 0.2f )
            {
                eated = false;
                animator_timer = 0;
            }
        }



        reyBehaviour.animator.SetBool("isEating", eated);
        animatoring.SetBool("isEated", eated);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Obtén el tag del objeto
        string foodTag = other.tag;

        reyBehaviour.food_list.Add(foodTag);

        if (reyBehaviour != null)
        {
            reyBehaviour.kingEated++;
            Debug.Log("he llegado aqui");

            if (foodTag == "Small")
            {
                eated = true;
                reyBehaviour.small_cnt++;
            }
            else if (foodTag == "Medium")
            {
                reyBehaviour.medion_cnt++;
                eated = true;
            }
            else if (foodTag == "Large")
            {
                reyBehaviour.large_cnt++;
                eated = true;
            }

            // Destruye el objeto original
            Destroy(other.gameObject);
        }
    }
}
