using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviou : MonoBehaviour
{

    // ------ MOVEMENT ------
    Rigidbody2D rb;
    public int movement_speed;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ------ MOVEMENT ------
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = dir.normalized * movement_speed;
    }

}
