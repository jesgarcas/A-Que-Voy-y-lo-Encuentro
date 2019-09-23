using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BNavMeshC : MonoBehaviour
{

    private BManager bManager;


    public NavMeshAgent agent;
    void Start()
    {
        
        bManager = GetComponent<BManager>();
    }

    public void Destination()
    {
        agent.SetDestination(bManager.target.transform.position);
    }
    public void Destination(Transform t)
    {
        agent.SetDestination(t.position);
    }
    public void Destination(Vector3 v)
    {
        agent.SetDestination(v);
    }

    public void Stop()
    {
        agent.SetDestination(transform.position);
    }
    public void StopMove(float f)
    {
        Invoke("Stop", f);
    }
    public void EnableMove(float f)
    {
        Invoke("Destination", f);
    }

}
