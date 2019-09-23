using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BStateMachineC : MonoBehaviour
{
    public MonoBehaviour BIdleC, BAttackC, BFollowC, BReturnC, BDrinkC, BInvokeC, BDieC;

    private MonoBehaviour currentStatus;


    private void Start(){}
    private void Update(){}
    private void Awake()
    {
        EnableState(BIdleC);
    }

    public void EnableState(MonoBehaviour newStat)
    {
        if (currentStatus != null)
        {
            currentStatus.enabled = false;

        }
        currentStatus = newStat;
        currentStatus.enabled = true;
    }

}