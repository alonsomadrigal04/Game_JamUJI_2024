using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    public Transform player;
    public string sceneToLoad = "You_lose";

    void Update()
    {
        if (player == null)
        {
            Debug.Log("¡El jugador no está presente!");

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
