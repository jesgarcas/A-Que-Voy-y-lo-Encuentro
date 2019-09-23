using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIdleC : MonoBehaviour
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
    private void Update()
    {
        CheckDistance();
    }
    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, bManager.target.transform.position) < 15f)
        {
            bManager.idleLookPos.transform.GetChild(0).gameObject.SetActive(true);
            AudioManager.am.BossSong();
            bStateMachineC.EnableState(bStateMachineC.BFollowC);
        }
        else
        {
            transform.LookAt(bManager.idleLookPos.transform.position);
            bManager.idleLookPos.transform.GetChild(0).gameObject.SetActive(false);
            //Si esta tranquilo, se reestablece.
            bManager.health = bManager.maxHealth;
            bManager.HealthBar();
            bManager.canDrink = true;
            bManager.drinking = false;
            bManager.skin.material.color = Color.white;
            bManager.mouth.material.color = Color.white;
            bManager.attackType = "Attack";
            bManager.agent.speed = 3.5f;
        }

    }
}
