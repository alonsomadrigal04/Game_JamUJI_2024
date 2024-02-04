using UnityEngine;

public class pickObjects : MonoBehaviour
{
    public Player_behaviou player_Behaviou;

    Collider2D playerCollider;
    public GameObject mainObject;
    public AudioClip pickSound;

    void Start()
    {
        playerCollider = player_Behaviou.maincollider;
        player_Behaviou = FindObjectOfType<Player_behaviou>();
    }

    private void PlayRandomDyingSound()
    {
        AudioSource.PlayClipAtPoint(pickSound, transform.position);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player_Behaviou.isCarry)
        {
            if (collision.gameObject.tag == "Small" || collision.gameObject.tag == "Medium" || collision.gameObject.tag == "Large")
            {
                mainObject = collision.gameObject;
                if(collision.gameObject.tag == "Small")
                {
                    mainObject.transform.position = player_Behaviou.gameObject.transform.position;
                }
                else if (collision.gameObject.tag == "Medium")
                {
                    mainObject.transform.position = player_Behaviou.gameObject.transform.position;
                }
                else if (collision.gameObject.tag == "Large")
                {
                    mainObject.transform.position = player_Behaviou.gameObject.transform.position;
                }
                mainObject.transform.SetParent(player_Behaviou.transform);
                player_Behaviou.isCarry = true;
                PlayRandomDyingSound();

                // Reducir la velocidad seg�n el tipo de objeto recogido
                if (collision.gameObject.tag == "Medium")
                {
                    player_Behaviou.movement_speed = 1.1f;
                }
                else if (collision.gameObject.tag == "Large")
                {
                    player_Behaviou.movement_speed = 0.8f;
                }
                else // Si no es Medium ni Large, mantener velocidad constante en 5
                {
                    player_Behaviou.movement_speed = 1.5f;
                }
            }
        }
    }
}
