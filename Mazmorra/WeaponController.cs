using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Collider weaponC;
    public AudioSource audioHit2;
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        weaponC.enabled = false;
        audioHit2.Play();
    }


}
