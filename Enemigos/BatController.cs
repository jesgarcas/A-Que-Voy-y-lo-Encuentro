using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BatController : MonoBehaviour
{
    //Bat se moverá libremente hasta que encuentre un objetivo.

    public GameObject fireBall;
    public float delay = 1.4f;
    public Transform shootPoint;
    public int maxHealth;
    public ParticleSystem hit,die;
    public AudioSource dieSound;
    public SkinnedMeshRenderer skin;


    public int health;
    Transform target;
    NavMeshAgent agent;
    float timer;
    Vector3 startPoint;

         
    void Start()
    {
        hit.Stop();die.Stop();
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }


    void Update()
    {
        
        
        timer += Time.deltaTime;
        if (!MazzManager.mm.dead)
        {
            Wandering();
        }
        else
        {
            agent.stoppingDistance = 0f;
            agent.SetDestination(startPoint);
        }
        
        Die();

    }
    void Wandering()
    {
        if (Vector3.Distance(transform.position,target.position)>10f)
        {
            agent.stoppingDistance = 0f;
            agent.SetDestination(startPoint);
        }
        else
        {
            agent.stoppingDistance = 5f;
            agent.SetDestination(target.position);
            transform.LookAt(new Vector3(target.position.x, 0.65f, target.position.z));

            if (Physics.Raycast(transform.position, target.position - transform.position, out RaycastHit hit) && hit.transform.CompareTag("Player"))
            {
                FireBall();
            }
            
        }
    }
    void FireBall()
    {
        if (timer >= delay)
        {
            timer = 0;
            Instantiate(fireBall, shootPoint.position,shootPoint.rotation);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pantufla")
        {
            if (!hit.isPlaying)
            {
                hit.Play();
            }
            health -= 20;
            UpdateLife();
        }
        if (other.tag == "Pantumerang")
        {
            if (!hit.isPlaying)
            {
                hit.Play();
            }
            health -= 10;
            UpdateLife();
        }
        
    }
    void Die()
    {
        if (health<=0)
        {
            timer = 0;
            skin.enabled = false;
            transform.position = transform.position;
            if (!die.isPlaying)
            {
                dieSound.Play();
                die.Play();
                //transform.GetChild(0).gameObject.SetActive(false);
            }
            Invoke("DieDestroy",0.7f);
            
        }
    }
    void DieDestroy()
    {
        Destroy(this.gameObject);
    }
    void UpdateLife()
    {
        transform.GetChild(0).GetComponent<EnemyBar>().UpdateHealth(health, maxHealth);
    }

}
