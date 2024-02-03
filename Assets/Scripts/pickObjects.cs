using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class pickObjects : MonoBehaviour
{
    public Player_behaviou player_Behaviou;
    Collider2D playerCollider;
    public GameObject mainObject;
    
    
    void Start()
    {
        playerCollider = player_Behaviou.maincollider;
        player_Behaviou = FindObjectOfType<Player_behaviou>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!player_Behaviou.isCarry)
        {
            if (collision.gameObject.tag == "Small" || collision.gameObject.tag == "Medium" || collision.gameObject.tag == "Large")
            {   
                mainObject = collision.gameObject;
                mainObject.transform.position = player_Behaviou.gameObject.transform.position + new Vector3(0,1);
                mainObject.transform.SetParent(player_Behaviou.transform);
                player_Behaviou.isCarry = true;
            }

        }
    }
}
