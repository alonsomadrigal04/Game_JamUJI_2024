using UnityEngine;

public class pickObjects : MonoBehaviour
{
    public Player_behaviou player_Behaviou;
    public RandomSpawn randomSpawn;

    Collider2D playerCollider;
    public GameObject mainObject;

    void Start()
    {
        randomSpawn = FindObjectOfType<RandomSpawn>();
        playerCollider = player_Behaviou.maincollider;
        player_Behaviou = FindObjectOfType<Player_behaviou>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player_Behaviou.isCarry)
        {
            if (collision.gameObject.tag == "Small" || collision.gameObject.tag == "Medium" || collision.gameObject.tag == "Large")
            {
                randomSpawn.quantityObjects -= 1;
                mainObject = collision.gameObject;
                mainObject.transform.position = player_Behaviou.gameObject.transform.position + new Vector3(0, 1);
                mainObject.transform.SetParent(player_Behaviou.transform);
                player_Behaviou.isCarry = true;

                // Reducir la velocidad según el tipo de objeto recogido
                if (collision.gameObject.tag == "Medium")
                {
                    player_Behaviou.movement_speed = 4;
                }
                else if (collision.gameObject.tag == "Large")
                {
                    player_Behaviou.movement_speed = 2;
                }
                else // Si no es Medium ni Large, mantener velocidad constante en 5
                {
                    player_Behaviou.movement_speed = 1.5f;
                }
            }
        }
    }
}
