using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeritaController : MonoBehaviour
{
    //Este script se encarga de gestionar la animación de Verita, así como dibujar el panel sobre el cual se dibuja el texto.

    [HideInInspector]
    public Animator anim;
    public GameObject jokes;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {

    }
    //Disparo la animación si recibe un true como parámetro
    public void Paper(bool b)
    {
        if (b)
        {
            anim.SetTrigger("TragaPapel");

        }

    }
    //Llamo a jokes para ejecutar su Método de dibujo
    public void TellJoke()
    {
        jokes.GetComponent<Jokes>().DrawJoke();
    }
    //Le indico a jokes que deshabilite el panel.
    public void DisablePanel()
    {
        jokes.GetComponent<Jokes>().DisablePanel();
    }
}
