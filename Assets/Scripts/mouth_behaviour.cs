using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderBocaScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Rey_behaviour reyBehaviour = GetComponentInParent<Rey_behaviour>();

        if (other.CompareTag("Small"))
        {
            if (reyBehaviour != null)
            {
                reyBehaviour.kingEated++;
                reyBehaviour.small_cnt++;
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Medium"))
        {
            if (reyBehaviour != null)
            {
                reyBehaviour.kingEated++;
                reyBehaviour.medion_cnt++;
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Large"))
        {
            if (reyBehaviour != null)
            {
                reyBehaviour.kingEated++;
                reyBehaviour.large_cnt++;
                Destroy(other.gameObject);
            }
        }

    }
}
