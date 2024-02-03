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
        if (collision.gameObject.tag == "objects")
        {   
            mainObject = collision.gameObject;
            mainObject.transform.position = player_Behaviou.gameObject.transform.position + new Vector3(0,1);
            mainObject.transform.SetParent(player_Behaviou.transform);
            
        }
    }
}