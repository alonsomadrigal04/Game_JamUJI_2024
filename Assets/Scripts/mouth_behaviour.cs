using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderBocaScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("objects"))
        {
            Rey_behaviour reyBehaviour = GetComponentInParent<Rey_behaviour>();

            if (reyBehaviour != null)
            {
                reyBehaviour.kingEated++;
                Destroy(gameObject);

            }
        }
    }
}
