using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public pickObjects pickObjectse;
    public Player_behaviou player_Behaviou;

    void Update()
    {
        pickObjectse = FindObjectOfType<pickObjects>();
        player_Behaviou = FindObjectOfType<Player_behaviou>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.transform.IsChildOf(player_Behaviou.transform))
        {
            Destroy(collision.gameObject);
        }
    }

}
