using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFountain : MonoBehaviour
{
    //La fuente comprueba si está el jugador cerca, en cuyo caso lo cura.

    public GameObject player;
    float delay = 0.1f;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (timer>=delay)
            {
                timer = 0;
                player.GetComponent<Player>().health += 1;
            }
            
        }
    }
}
