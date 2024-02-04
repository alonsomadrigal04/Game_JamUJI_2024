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
    public float animator_timer = 0.5f;
    public bool attacked;


    private void Start()
    {
        pickObjectse = FindObjectOfType<pickObjects>();
        player_Behaviou = FindObjectOfType<Player_behaviou>();
        
    }
    void Update()
    {
        controlDirection();
        shootItem();
        counterTimer();
    }

    public void counterTimer()
    {
        if(attacked)
        {
            animator_timer += Time.deltaTime;
            if (animator_timer > 0.5f)
            {
                attacked = false;
                player_Behaviou.animator.SetBool("IsAttacking", false);
                animator_timer = 0;
            }
        }
    }

    private void shootItem()
    {
        switch (controlKey)
        {
            case "A":
                directionVector = new Vector2(-1f, 0f);
                player_Behaviou.spriteRenderer.flipX = false;
                break;
            case "S":
                directionVector = new Vector2(0f, -1f);
                break;
            case "D":
                directionVector = new Vector2(1f, 0f);
                player_Behaviou.spriteRenderer.flipX = true;
                break;
            case "W":
                directionVector = new Vector2(0f, 1f);
                break;
            default: break;
        }

        if(player_Behaviou.isCarry)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                attacked= true;
                player_Behaviou.isCarry = false;
                GameObject pickedObject = pickObjectse.mainObject;
                pickedObject.transform.parent = null;
                pickedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, velocity) * directionVector;
                
                player_Behaviou.animator.SetBool("IsAttacking", true);

                

            }

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
