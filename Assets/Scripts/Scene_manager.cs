using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using TMPro;

public class Scene_manager : MonoBehaviour
{
    public Transform player;
    public string sceneToLoad = "You_lose";


    public Rey_behaviour scrRey;
    public TMP_Text contadorText;

    void Update()
    {
        if (player == null)
        {

            SceneManager.LoadScene(sceneToLoad);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (scrRey != null && contadorText != null)
        {
            // Actualizar el texto con el valor del contador
            if(scrRey.phase_timer >= 5 || scrRey.phase_timer <= 0)
            {
                contadorText.text = null;
            }
            else
            {
                contadorText.text = Mathf.FloorToInt(scrRey.phase_timer).ToString();

            }

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
