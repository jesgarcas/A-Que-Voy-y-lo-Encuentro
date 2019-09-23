using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BInvokeC : MonoBehaviour
{
    private BStateMachineC bStateMachineC;
    private BNavMeshC bNavMeshC;
    private BManager bManager;
    private bool invoking;



    private void Awake()
    {
        bStateMachineC = GetComponent<BStateMachineC>();
        bNavMeshC = GetComponent<BNavMeshC>();
        bManager = GetComponent<BManager>();
        invoking = false;

    }
    private void Start()
    {

    }
    private void Update()
    {
        bNavMeshC.Stop();
        if (!invoking)
        {
            invoking = true;
            bManager.audioChicken.Play();
            bManager.anim.SetTrigger("Invoke");
            bManager.Inmortality();
            //Invocacion
            Instantiate(bManager.chicken, bManager.invokePos1.position, transform.rotation);
            Instantiate(bManager.chicken, bManager.invokePos2.position, transform.rotation);


            Invoke("Mortality", 1.9f);
            Invoke("ChangeToFollow", 2f);
            
        }
    }
    void Mortality()
    {
        bManager.Mortality();
    }
    void ChangeToFollow()
    {
        invoking = false;
        bStateMachineC.EnableState(bStateMachineC.BFollowC);
    }
}
