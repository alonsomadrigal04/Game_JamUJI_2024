using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePicked : MonoBehaviour
{
    public pickObjects pickObjectse;
    public Player_behaviou player_Behaviou;
    public float velocity;
    private string controlKey;
    private Vector2 directionVector;


    private void Start()
    {
        pickObjectse = FindObjectOfType<pickObjects>();
        player_Behaviou = FindObjectOfType<Player_behaviou>();
    }
    void Update()
    {
        controlDirection();
        shootItem();
    }

    private void shootItem()
    {
        switch (controlKey)
        {
            case "A":
                directionVector = new Vector2(-1f, 0f);
                break;
            case "S":
                directionVector = new Vector2(0f, -1f);
                break;
            case "D":
                directionVector = new Vector2(1f, 0f);
                break;
            case "W":
                directionVector = new Vector2(0f, 1f);
                break;
            default: break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player_Behaviou.isCarry = false;
            GameObject pickedObject = pickObjectse.mainObject;
            pickedObject.transform.parent = null;
            pickedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, velocity) * directionVector;
        }
    }

    private void controlDirection()
    {
        if (Input.GetKeyDown(KeyCode.A))
            controlKey = "A";
        else if (Input.GetKeyDown(KeyCode.D))
            controlKey = "D";
        else if(Input.GetKeyDown(KeyCode.S))
            controlKey = "S";
        else if (Input.GetKeyDown(KeyCode.W))
            controlKey = "W";
    }
}
