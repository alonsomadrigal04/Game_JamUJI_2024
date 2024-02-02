using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviou : MonoBehaviour
{

    // ------ MOVEMENT ------
    Rigidbody2D rb;
    public int movement_speed;

    //------- CARRY --------
    public bool isCarry;
    public Collider2D maincollider;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        maincollider = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        // ------ MOVEMENT ------
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = dir.normalized * movement_speed;
    }

}
