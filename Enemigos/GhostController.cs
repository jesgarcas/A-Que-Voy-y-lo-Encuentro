using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class    GhostController : MonoBehaviour
{


    bool following,dying;
    Transform playerTransform;
    float distance;
    Animator anim;
    int health;


    public int maxHealth;
    public float speed;
    public ParticleSystem spawnEff,attEff,dieEff,hitEff;
    public GameObject attackCollider;
    public GameObject ghostMesh;
    public AudioSource dieS;

    private void Start()
    {
        spawnEff.Stop();attEff.Stop();dieEff.Stop();hitEff.Stop();
        dying = false;
        health = maxHealth;
        following = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    private void OnEnable()
    {
        
        if (!spawnEff.isPlaying)
        {
            spawnEff.Play();
        }
    }
    private void Update()
    {
        

        if (!MazzManager.mm.dead)
        {
            distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance < 10f)
            {
                following = true;
                anim.SetBool("Walking", true);
            }

            if (distance < 1.5f)
            {
                Attack();
                following = false;
                anim.SetBool("Walking", false);
            }
            if (following && !dying)
            {
                transform.LookAt(playerTransform.transform);
                Navigation();
            }
        }
    }

    void Navigation()
    {

        
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, 0.40f, transform.position.z);
    }
    void Attack()
    {
        if (!attEff.isPlaying && !dying)
        {
            attEff.Play();
            attackCollider.SetActive(true);
            anim.SetTrigger("Attack");
            Invoke("DisableAttackCollider",1f);
        }
    }

    void DisableAttackCollider()
    {
        attackCollider.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pantufla")
        {
            if (!hitEff.isPlaying)
            {
                hitEff.Play();
                health -= 20;
            }
        }
        if (other.tag == "Pantumerang")
        {
            if (!hitEff.isPlaying)
            {
                hitEff.Play();
                health -= 10;
            }
        }
        
        if (health <= 0)
        {
            GetComponent<SphereCollider>().enabled = false;
            //transform.GetChild(0).gameObject.SetActive(false);
            dieS.Play();
            dying = true;
            following = false;
            transform.position = transform.position;
                
            anim.SetTrigger("Die");

            Invoke("DieEffect", 2f);
        }
        UpdateLife();
       
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
    void DieEffect()
    {
        dieEff.Play();
        ghostMesh.SetActive(false);
        Invoke("Die", 2f);
    }

    void UpdateLife()
    {
        transform.GetChild(0).GetComponent<EnemyBar>().UpdateHealth(health, maxHealth);
    }

}
