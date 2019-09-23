using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BReturnC : MonoBehaviour
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
        bNavMeshC.Destination(bManager.spawnPosition);
        bManager.bossBar.SetActive(false);
        if (bNavMeshC.agent.velocity.magnitude > 0.5f)
        {
            bManager.anim.SetBool("Walking", true);
        }
        else
        {
            bManager.anim.SetBool("Walking", false);
        }
        if (Vector3.Distance(transform.position,bManager.spawnPosition)<2f)
        {
            bManager.anim.SetBool("Walking", false);
            AudioManager.am.MazzSong();
            bStateMachineC.EnableState(bStateMachineC.BIdleC);
        }

    }
}
