using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController1 : MonoBehaviour
{

    //Este script corresponde a la palanca que desbloquea la primera Zona, para ello cambia una variable a True en el Singleton MazzManager.

    public Animator anim;
    public AudioSource sound;
    bool aux;
   
    private void Start()
    {
        aux = true;
        sound = GetComponent<AudioSource>();
    }
    public void Action()
    {
        if (aux)
        {
            aux = false;
            anim.SetTrigger("isOn");
            Invoke("Camera", 0.3f);
        }

    }
    
    //sound on
    public void SoundOn()
    {
        sound.Play();
      
    }
    void Camera()
    {
        
        MazzManager.mm.door1 = true;
    }

    public void DestruyePollo()
    {
        //
    }
}
