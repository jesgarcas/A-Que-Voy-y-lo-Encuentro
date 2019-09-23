using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDrinkC : MonoBehaviour
{
    private BStateMachineC bStateMachineC;
    private BNavMeshC bNavMeshC;
    private BManager bManager;




    private void Awake()
    {
        bStateMachineC = GetComponent<BStateMachineC>();
        bNavMeshC = GetComponent<BNavMeshC>();
        bManager = GetComponent<BManager>();

    }
    private void Start()
    {

    }
    private void Update()
    {
        bNavMeshC.Stop();
        transform.LookAt(bManager.targetTest.transform.position);
        if (!bManager.drinking)
        {
            bManager.audioDrink.Play();
            bManager.Inmortality();
            bManager.drinking = true;
            bManager.anim.SetTrigger("Heal");

            Invoke("BerserkerChanges", 5.1f);
            Invoke("CancelShield", 8.2f);
            Invoke("ChangeToFollow", 8.3f);
            
        }
       

    }
    void BerserkerChanges()
    {
        //Cosas del Berserker
        bManager.audioFury.Play();
        bManager.attackType = "Attack2";
        bManager.agent.speed = 5;
        bManager.skin.material.color = Color.red;
        bManager.mouth.material.color = Color.red;
    }
    void CancelShield()
    {
        bManager.Mortality();
    }
    void ChangeToFollow()
    {
        bManager.drinking = false;
        bStateMachineC.EnableState(bStateMachineC.BFollowC);
    }
}
