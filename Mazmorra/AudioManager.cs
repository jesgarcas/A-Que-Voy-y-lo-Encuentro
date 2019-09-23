using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    public AudioSource mazzSong,bossSong;
    void Start()
    {
        am = this;
        MazzSong();
    }

    void Update()
    {
        
    }

    public void BossSong()
    {
        mazzSong.enabled = false;
        bossSong.enabled = true;
    }
    public void MazzSong()
    {
        mazzSong.enabled = true;
        bossSong.enabled = false;
    }



    
}
