using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAttackC : MonoBehaviour
{
    private BStateMachineC bStateMachineC;
    private BNavMeshC bNavMeshC;
    private BManager bManager;
    bool attacking;
    bool enableMove;



    private void Awake()
    {
        bStateMachineC = GetComponent<BStateMachineC>();
        bNavMeshC = GetComponent<BNavMeshC>();
        bManager = GetComponent<BManager>();
        attacking = false;
        
        
        

    }

    private void Update()
    {
        bNavMeshC.Stop();
        Attack();
        CheckDistance();


    }

    void Attack()
    {
        if (!attacking)
        {
           
            attacking = true;
            enableMove = false;
            transform.LookAt(bManager.targetTest.transform);
            bManager.anim.SetTrigger(bManager.attackType);
            Invoke("EnableGuitar", 0.2f);
            

            //Restablecemos su estado anterior para que pueda atacarnos de nuevo
            Invoke("DisableGuitar", 1.2f);
            Invoke("EnableAttack", 1.5f);
            Invoke("EnableMove", 1.4f);
        }
        

    }
    void EnableGuitar()
    {
        bManager.audioGuitar.Play();
        bManager.guitar.enabled = true;
    }
    void DisableGuitar()
    {
        bManager.guitar.enabled = false;
        
    }
    void EnableAttack()
    {
        attacking = false;
    }
    void EnableMove()
    {
        bManager.audioGuitar.Stop();
        enableMove = true;
    }
    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, bManager.target.transform.position) >= 3f && enableMove)
        {
            bStateMachineC.EnableState(bStateMachineC.BFollowC);
        }
    }
}
