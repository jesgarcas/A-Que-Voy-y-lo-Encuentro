using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Baldomero : MonoBehaviour
{

    //# DEPRECATED

    //Este script era el que controlaba originalmente a Baldomero.
    //Dado la complegidad que estaba adquiriendo, se ha reemplazado por una máquina de estados.
    //Se ha mantenido para hacer consultas a ciertos comportamientos.

    NavMeshAgent agent;
    Vector3 spawnPoint;
    Animator anim;
    bool canMove,attacking,invoking,dying;
    int health;
    string attackType = "Attack";

    public int howMuchTequila;
    public int maxInvokes;
    public int maxHealth;
    public bool canInvoke;
    public Image healthBar;
    public GameObject bossBar;
    public GameObject target;
    public SkinnedMeshRenderer skin;
    public MeshRenderer mouth;
    public BoxCollider guitar;
    public Transform eyes;
    public ParticleSystem shield;
    public ParticleSystem hit;
  
    void Start()
    {
        StopAnims();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        spawnPoint = transform.position;
        canMove = true;
        invoking = false;
        dying = false;
        health = maxHealth;
    }
    private void OnEnable()
    {
        StopAnims();
    }

    void Update()
    {
        //canMove es false solo mientras ataca
        if (canMove)
        {
            StopShield();
            BackToOffice();
            TargetFollow(); 
        }
        if (!invoking)
        {
            Invoke();
        }
        

        AnimControl();
        Attack();
        Berserker();
        Die();
    }


    void BackToOffice()
    {
        Debug.DrawRay(eyes.position, eyes.forward, Color.green);
        Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hit);

        if (Vector3.Distance(transform.position,target.transform.position)>10f && !hit.transform)
        {
            bossBar.SetActive(false);
            agent.SetDestination(spawnPoint);
        }
    }
    void TargetFollow()
    {
        if (Vector3.Distance(transform.position,target.transform.position)<10f)
        {
            bossBar.SetActive(true);
            agent.SetDestination(target.transform.position);
        }
    }
    void AnimControl()
    {
        //Movimiento
        if (agent.velocity.magnitude > 0.5f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    void Attack()
    {
        //ataque
        if (Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance + 0.5f && !attacking && health > 0)
        {
            //Especificamos que estamos atacando para evitar que se ejecute esto varias veces
            attacking = true;
            //Impedimos que pueda moverse, por si intentamos evadir un ataque, que no nos siga en su ejecución.
            StopMove();
            transform.LookAt(target.transform.position);
            anim.SetTrigger(attackType);
            //Esperamos un poco para que coincida la animación con el daño a la vida
            Invoke("EnableGuitar", 0.7f);

            //Restablecemos su estado anterior para que pueda atacarnos de nuevo
            Invoke("EnableMove", 1.4f);
            Invoke("DisableGuitar", 1.4f);

        }
    }


    void DisableGuitar()
    {
        attacking = false;
        guitar.enabled = false;
    }
    void EnableGuitar()
    {
        guitar.enabled = true;
    }



    //Salud
    void Die()
    {

        if (health ==10 && !attacking && !dying)
        {
            dying = true;
            StopMove();
            anim.SetTrigger("Die");
            Inmortality();
            Invoke("Inmortality",1f);
        }
        if (health <=0 && !attacking)
        {
            SceneManager.LoadScene(3);
        }
    } 

    void Inmortality()
    {
        if (!shield.isPlaying)
        {
            shield.Play();
        }
        GetComponent<CapsuleCollider>().enabled = !GetComponent<CapsuleCollider>().enabled;
    }
    void StopShield()
    {
            shield.Stop();
    }

    void DieAnim()
    {
        agent.baseOffset = -2.5f;
    }
    void Invoke()
    {
        if (canInvoke && maxInvokes >0 && !attacking)
        {
            maxInvokes -= 1;

            StopMove();
            Inmortality();

            anim.SetTrigger("Invoke");
            //Invocacion
            //
            //
            //
            //Fin invocacion
            Invoke("FinishInvoke", 5.3f);
            Invoke("EnableMove", 5.3f);
            Invoke("Inmortality", 5.3f);

        }

    }
    void FinishInvoke()
    {
        invoking = false;
    }



    void StopMove()
    {
        agent.SetDestination(transform.position);
        canMove = false;
    }
    void EnableMove()
    {
        if (health >0)
        {
            canMove = true;
        }
        
    }
    void DestroyBaldomero()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pantufla")
        {

            if (!hit.isPlaying)
            {
                hit.Play();
                health -= 5;
                HealthBar();
            }
        }
    }
    void StopAnims()
    {
        shield.Stop(); hit.Stop();
    }

    void HealthBar()
    {
        healthBar.fillAmount = health /100f;
    }
    void Berserker()
    {
        if (health <= 40 && !attacking && howMuchTequila > 0)
        {
            print("Ahora te vas a enterar");
            howMuchTequila -= 1;
            Inmortality();
            StopMove();

            anim.SetTrigger("Heal");
            Invoke("EnableMove", 8.3f);
            Invoke("Inmortality", 8.3f);

            Invoke("BerserkerChanges", 4.1f);
        }
    }
    void BerserkerChanges()
    {
        //Cosas del Berserker
        attackType = "Attack2";
        agent.speed = 5;
        skin.material.color = Color.red;
        mouth.material.color = Color.red;
    }
}
