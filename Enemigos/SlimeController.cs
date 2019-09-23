using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{


    public GameObject slimePrefab, attackCollider, slimeContainer, slimeSpawn;
    public ParticleSystem hitEff, attEff, dieEff;
    public SkinnedMeshRenderer skin;
    public bool mustReturn;
    [Header("Audios")]
    public AudioSource audioDie, audioDivide;
    public AudioClip[] slimeS;
    
    

    Transform target;
    NavMeshAgent agent;
    Animator anim;
    Vector3 startPoint;
    int size = 3;
    float health;
    float maxHealth;
    AudioSource audioS;

    






    void Start()
    {
        //Impedimos que comience con las partículas habilitadas.
        hitEff.Stop(); attEff.Stop(); dieEff.Stop();
        audioS = GetComponent<AudioSource>();
        audioS.clip = slimeS[size - 1];
        audioS.Play();
        audioDivide.Play();


        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        startPoint = slimeSpawn.transform.position;
        health = 10 * size;
        maxHealth = 10 * size;



    }
    private void OnEnable()
    {
        //Impedimos que comience con las partículas habilitadas.
        hitEff.Stop(); attEff.Stop(); 
    }

    void Update()
    {
        if (!MazzManager.mm.dead || !mustReturn)
        {
            Detection();
        }
        else
        {
            agent.SetDestination(startPoint);
        }
        UpdateLife();
        Anims();
    }

    void Detection()
    {
        
        if (Vector3.Distance(target.position,transform.position)<=10f && !MazzManager.mm.dead && !mustReturn)
        {
            agent.SetDestination(target.position);
            
            Attack();
        }
        else
        {
            agent.SetDestination(startPoint);
        }

        if (Vector3.Distance(startPoint,transform.position)<=1)
        {
            mustReturn = false;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(target.position,transform.position)<2.5f)
        { 
            if (!attEff.isPlaying)
            {
                attackCollider.SetActive(true);
                anim.SetTrigger("Attack");
                attEff.Play();
                Invoke("DisableAttackCollider",1f);
            }
        }
    }


    void Anims()
    {

        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    public void Health(int i)
    {
        health += i;
    }
    void Death()
    {
        if (health <= 0)
        {
            audioDie.Play();
            //transform.GetChild(0).gameObject.SetActive(false);
            if (size >1)
            {
                size -= 1;
                GameObject slime = Instantiate(slimePrefab, transform.position, transform.rotation,slimeContainer.transform);
                GameObject slime2 = Instantiate(slimePrefab, transform.position, transform.rotation,slimeContainer.transform);

                slime.GetComponent<SlimeController>().Resize(size);
                slime2.GetComponent<SlimeController>().Resize(size);
               
            }

            Die();
            

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pantufla")
        { 
            if (!hitEff.isPlaying)
            {
                hitEff.Play();
                health -= 10;
                Death();
            }
        }
        if (other.tag == "Pantumerang")
        {

            if (!hitEff.isPlaying)
            {
                hitEff.Play();
                health -= 5;
                Death();
            }

        }
        if (other.tag == "Player")
        {
            Attack();
        }
        if (other.tag == "SlimeReturn")
        {
            mustReturn = true;
        }

        UpdateLife();
    }

    void Resize(int i)
    {
        size = i;
        transform.localScale = new Vector3(i,i,i);
    }
     
    
    void Die()
    {
        agent.destination = transform.position;
        transform.position = transform.position;
        GetComponent<CapsuleCollider>().enabled = false;
        skin.enabled = false;
        dieEff.Play();
        Destroy(this.gameObject,0.8f);
    }
    void DisableAttackCollider()
    {
        attackCollider.SetActive(false);
    }
    void UpdateLife()
    {
        transform.GetChild(0).GetComponent<EnemyBar>().UpdateHealth(health, maxHealth);
    }
}
