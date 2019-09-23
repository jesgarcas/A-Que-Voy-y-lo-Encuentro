using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenAttack : MonoBehaviour
{

    //Este script controla las Gallinas que invoca Baldomero.
    //Su función es, ser invulnerable mientras aparecen, y perseguir al jugador mediante un NavMeshAgent.
    //Cuando choca con el jugador o su arma, desaparecen.

    public ParticleSystem eff,die;
    public SkinnedMeshRenderer skin;
    public GameObject eyes;
    public GameObject audioSpawn;
    public AudioSource audioDie;

    GameObject target;
    SphereCollider sCollider;
    NavMeshAgent agent;
    float speed;
    bool dying;
    
    void Start()
    {
        dying = false;
        die.Stop();
        target = GameObject.FindGameObjectWithTag("Player");
        sCollider = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        speed = 4;
        agent.speed = speed;


    }

    void Update()
    {
        //Este bool sirve para saber cuando ha muerto el jugador, en este caso, eliminamos a las gallinas.
        if (MazzManager.mm.dead)
        {
            Destroy(this.gameObject);
        }

        if (!eff.isPlaying && !dying && !MazzManager.mm.dead)
        {
            
            sCollider.enabled = true;
            agent.SetDestination(target.transform.position);
            audioSpawn.SetActive(true); ;

            //Para que de la sensación de que levanta el vuelo
            if (agent.baseOffset< 1.5f)
            {
                agent.baseOffset += 1.5f * Time.deltaTime;
            }
            
        }
        if(dying && !MazzManager.mm.dead)
        {
            
           agent.SetDestination(transform.position);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //Si choca con cualquier collider que no sea el de Baldomero, y no esta haciendo la animación de Muerte,
        //Deshabilitamos gran parte de los componentes hasta que termine de ejecutarse la particula de muerte, 
        //entonces, se elimina.
        if (other.tag != "Baldomero")
        {
            if (!die.isPlaying)
            {
                audioDie.Play();
                sCollider.enabled = false;
                eyes.SetActive(false);
                dying = true;
                die.Play();
                skin.enabled = false;
                Invoke("Die", die.main.duration);
            }

        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
}
