using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BManager : MonoBehaviour
{
    //En este script gestiono todas las variables que requiere Baldomero, como los sonidos, la vida, los objetivos y sus partículas.
    
    private BStateMachineC bStateMachineC;
    private bool dying = false;
    [Header("Debug Info")]
    public float timer;
    [Header("Sounds")]
    public AudioSource audioDrink;
    public AudioSource audioFury;
    public AudioSource audioDying;
    public AudioSource audioChicken;
    public AudioSource audioGuitar;

    [Header("Targets")]
    public BoxCollider guitar;
    public GameObject target;
    public GameObject targetTest;
    public GameObject idleLookPos;

    [Header("OwnVars")]
    public SkinnedMeshRenderer skin;
    public MeshRenderer mouth;
    public Image healthBar;
    public GameObject bossBar;
    public int maxHealth;

    [Header("Particles")]
    public ParticleSystem shield;
    public ParticleSystem hit;

    [Header("Invokes")]
    public float invokeTime;
    public GameObject chicken;
    public Transform invokePos1, invokePos2;

    //Variables Publicas no editables desde el Inspector
    #region
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public string attackType;
    [HideInInspector]
    public Vector3 spawnPosition;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool canDrink = true;
    [HideInInspector]
    public bool drinking = false;
    [HideInInspector]
    public bool isNear = false;
    [HideInInspector]
    public float health;




    #endregion

    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        attackType = "Attack";
        spawnPosition = transform.position;
        timer = invokeTime;
        bStateMachineC = GetComponent<BStateMachineC>();
        agent = GetComponent<NavMeshAgent>();
        StopAnims();
    }

    //Hay tres estados que son imperativos, a los cuales se debe poder acceder desde cualquier estado.
    //Estos son, Invocar cada X Segundos.
    //
    void Update()
    {
        timer -= Time.deltaTime;
        if (Vector3.Distance(transform.position, target.transform.position) < 10f)
        {
            isNear = true;
        }
        else
        {
            isNear = false;
        }

        if (timer <0 && !dying && !drinking && isNear)
        {
            timer = invokeTime;
            bStateMachineC.EnableState(bStateMachineC.BInvokeC);
        }

        if (health <=40 && canDrink && isNear)
        {
            canDrink = false;
            bStateMachineC.EnableState(bStateMachineC.BDrinkC);
        }
        if (health <=10 && !dying && isNear)
        {
            health = 5;
            dying = true;
            bStateMachineC.EnableState(bStateMachineC.BDieC);
        }
    }
    public void Inmortality()
    {
        if (!shield.isPlaying)
        {
            shield.Play();
        }
        GetComponent<CapsuleCollider>().enabled = false;
    }
    public void Mortality()
    {
        shield.Stop();
        GetComponent<CapsuleCollider>().enabled = true;

    }
    void StopAnims()
    {
        shield.Stop();hit.Stop();
    }
    public void HealthBar()
    {
        healthBar.fillAmount = health / 100f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pantufla")
        {
            if (!hit.isPlaying)
            {
                hit.Play();
                health -= 10;
                HealthBar();
            }
        }
        if (other.tag == "Pantumerang")
        {
            if (!hit.isPlaying)
            {
                hit.Play();
                health -= 5;
                HealthBar();
            }
        }
    }
}
