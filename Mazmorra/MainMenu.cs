using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Este script está presente en el Menu principal del juego.
    //Su función es la de hacer una carga asyncrona en una corutina para reducir la bajada de FPS al cargar la escena de la Villa.

    public Animator anim;
    bool keyE;
    void Start()
    {
        keyE = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (keyE)
            {
                keyE = false;
                StartCoroutine(LoadYourAsyncScene());
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
    public void CreditsIn()
    {
        anim.SetTrigger("CreditsIn");
    }
    public void CreditsOut()
    {
        anim.SetTrigger("CreditsOut");
    }
    IEnumerator LoadYourAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(4);

           //Esperamos hasta que la escena esté completamente cargada.
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
