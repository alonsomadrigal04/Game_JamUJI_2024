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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

    }
        private void QuitGame()
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                                Application.Quit();
            #endif
        }
}
