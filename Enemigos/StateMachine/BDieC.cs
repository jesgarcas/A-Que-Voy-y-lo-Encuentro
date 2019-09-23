using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BDieC : MonoBehaviour
{
    private BManager bManager;
    private BNavMeshC bNavMeshC;
    private bool die = true;
    private void Awake()
    {
        bManager = GetComponent<BManager>();
        bNavMeshC = GetComponent<BNavMeshC>();

    }
    void Start()
    {

    }
    void Update()
    {
        bNavMeshC.Stop();
        Invoke("LookPlayer", 2.52f);
        if (die)
        {
            Die(die);
        }
       

        if (bManager.health <= 0)
        {
            SceneManager.LoadScene(3);
        }

    }
    void Die(bool b)
    {
        if (b)
        {
            die = !b;
            bManager.anim.SetTrigger("Die");
            bManager.audioDying.Play();
            bManager.Inmortality();
            Invoke("Mortality", 4f);
        }

    }

    void Mortality()
    {
        bManager.Mortality();
    }
    void LookPlayer()
    {
        transform.LookAt(bManager.targetTest.transform.position);
    }
}
