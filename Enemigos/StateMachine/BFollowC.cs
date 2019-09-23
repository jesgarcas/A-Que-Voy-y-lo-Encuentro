using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFollowC : MonoBehaviour
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
        bManager.bossBar.SetActive(true);
        bNavMeshC.Destination(bManager.target.transform);
        //Movimiento
        if (bNavMeshC.agent.velocity.magnitude > 0.5f)
        {
            bManager.anim.SetBool("Walking", true);
        }
        else
        {
            bManager.anim.SetBool("Walking", false);
        }
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, bManager.target.transform.position) <= 1.15f + 1f)
        {   
            bManager.anim.SetBool("Walking", false);
            bNavMeshC.Stop();
            bStateMachineC.EnableState(bStateMachineC.BAttackC);
        }
        if (Vector3.Distance(transform.position, bManager.target.transform.position) > 15f)
        {
            bManager.anim.SetBool("Walking", false);
            bNavMeshC.Stop();
            bStateMachineC.EnableState(bStateMachineC.BReturnC);
        }
    }

}
