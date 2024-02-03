using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderBocaScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Rey_behaviour reyBehaviour = GetComponentInParent<Rey_behaviour>();

        // Obtén el tag del objeto
        string foodTag = other.tag;

        reyBehaviour.food_list.Add(foodTag);

        if (reyBehaviour != null)
        {
            reyBehaviour.kingEated++;

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
            Destroy(other.gameObject);
        }
    }
}
