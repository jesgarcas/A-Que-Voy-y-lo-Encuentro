using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    public float rotationForce, speed;
    public int maxHealth;
    public GameObject prefab,shootPoint, weapon;
    public bool hidePantufla;
    public bool isShooting;
    public Image healthUI;
    public GameObject helpE,dot;
    public GameObject inputField;
    public InputField inputText;

    [Header("Audios")]
    public AudioSource audioWrite;
    public AudioSource audioDie;
    public AudioSource audioHit;
    public AudioSource audioWalking;
    public AudioSource audioKey;

    [Header("Images")]
    public GameObject iKeyA;
    public GameObject iKeyB;
    public GameObject iKeyC;
    [Header ("GameObjects")]
    public GameObject keyA;
    public GameObject keyB;
    public GameObject keyC;
    public GameObject respawnPoint;
    public GameObject verita;
    
    

    Transform playerCam;
    //Cosas de camara
    public float xMoveThreshold = 100.0f;
    public float yMoveThreshold = 100.0f;

    public float yMaxLimit;
    public float yMinLimit;

    [HideInInspector]
    public float health;
    [HideInInspector]
    public string inputNumber;




    [Header("Debug")]
    public bool alwaysMove;

    
    bool havePaper;
    bool attacking;
    string tagName;
    float yRotCounter = 0.0f;
    float xRotCounter = 0.0f;
    NavMeshAgent navMeshAgent;
    Animator anim;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        inputText.text = "";
        health = maxHealth;
        havePaper = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        hidePantufla = false;
        isShooting = false;
        attacking = false;
        anim = weapon.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            Control();
            Death();
            UpdateHealth(health);
            InputE();
        }

    }

    //Interacción con la E
    void InputE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (tagName)
            {
                case "KeyA":
                    audioKey.Play();
                    MazzManager.mm.keyA = true;
                    iKeyA.SetActive(true);
                    keyA.GetComponent<SphereCollider>().radius = 0.001f;
                    keyA.transform.position = Vector3.zero;
                    Destroy(keyA.gameObject, 0.2f);
                    break;

                case "KeyB":
                    audioKey.Play();
                    MazzManager.mm.keyB = true;
                    iKeyB.SetActive(true);
                    keyB.GetComponent<SphereCollider>().radius = 0.001f;
                    keyB.transform.position = Vector3.zero;
                    Destroy(keyB.gameObject, 0.2f);
                    break;

                case "KeyC":
                    audioKey.Play();
                    MazzManager.mm.keyC = true;
                    iKeyC.SetActive(true);
                    keyC.GetComponent<SphereCollider>().radius = 0.001f;
                    keyC.transform.position = Vector3.zero;
                    Destroy(keyC.gameObject, 0.2f);
                    break;

                case "Table":
                    if (inputText.text == "")
                    {
                        MazzManager.mm.canMove = false;
                        inputField.SetActive(true);
                        inputText.Select();
                        inputText.ActivateInputField();
                    }
                    break;

                case "Verita":
                    if (inputText.text != "")
                    {
                        verita.GetComponent<VeritaController>().Paper(havePaper);
                        havePaper = false;
                        CheckPaper();
                    }

                    break;

                case "palancaBuena":
                    GameObject.FindGameObjectWithTag("palancaBuena").GetComponent<LevelController1>().Action();
                    GameObject.FindGameObjectWithTag("palancaBuena").GetComponent<BoxCollider>().enabled = false;
                    break;

                case "palancaMala":
                    GameObject.FindGameObjectWithTag("palancaMala").GetComponent<LevelControllerFake>().Action();
                    GameObject.FindGameObjectWithTag("palancaMala").GetComponent<BoxCollider>().enabled = false;
                    HelpUI(false);
                    //helpE.SetActive(false);
                    //dot.SetActive(true);
                    break;

                default:
                    break;
            }
        }
    }

    //Input Controls
    void Control()
    {
        //Para evitar que se desajuste la animación con los cambios de cámara de las cinemáticas.
        if (isShooting)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }

        //Si el Manager nos permite movernos
        if (MazzManager.mm.canMove || alwaysMove)
        {
            //Cosas de camara
            xRotCounter += Input.GetAxis("Mouse X") * xMoveThreshold * Time.deltaTime;
            yRotCounter += Input.GetAxis("Mouse Y") * yMoveThreshold * Time.deltaTime;
            yRotCounter = Mathf.Clamp(yRotCounter, yMinLimit, yMaxLimit);
            //xRotCounter = xRotCounter % 360;//Optional
            playerCam.localEulerAngles = new Vector3(-yRotCounter, xRotCounter, 0);


            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                anim.SetBool("Walking", true);
                if (!audioWalking.isPlaying)
                {
                    audioWalking.Play();
                }

            }
            else
            {
                anim.SetBool("Walking", false);
                audioWalking.Stop();
            }


            if (hidePantufla)
            {
                weapon.GetComponent<BoxCollider>().enabled = false;
                weapon.transform.GetChild(0).gameObject.SetActive(false);
                //weapon.SetActive(false);
            }
            else
            {
                //weapon.GetComponent<BoxCollider>().enabled = true;
                weapon.transform.GetChild(0).gameObject.SetActive(true);
                //weapon.SetActive(true);
            }

            navMeshAgent.Move(playerCam.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
            navMeshAgent.Move(playerCam.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);



            if (Input.GetButtonDown("Fire2") && !isShooting && !attacking)
            {

                Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);
                isShooting = true;
            }
            if (Input.GetButtonDown("Fire1") && !hidePantufla)
            {
                weapon.GetComponent<BoxCollider>().enabled = true;
                attacking = true;
                anim.SetTrigger("Attacking");
                //Ataque principal
                audioHit.Play();
                Invoke("DisableWeapon", 0.5f);

            }
        }
    }

    //Cuando morimos.
    void Death()
    {
        if (health <= 0)
        {
            MazzManager.mm.dead = true;
            audioDie.Play();
            MazzManager.mm.EnableRespawnPanel(false);
            health = 8.5f;
            navMeshAgent.enabled = false;
            transform.position = respawnPoint.transform.position;
            transform.LookAt(transform.position + Vector3.forward);
            navMeshAgent.enabled = true;

            inputField.SetActive(false);
            HelpUI(false);
            //helpE.SetActive(false);
            //dot.SetActive(true);

            Invoke("DisableDeath", 10f);

        }
    }
    void DisableDeath()
    {
        MazzManager.mm.dead = false;
    }

    void DisableWeapon()
    {
        attacking = false;
        weapon.GetComponent<BoxCollider>().enabled = false;
    }
    public void UpdateHealth(float f)
    {

        if (f >= 100f)
        {
            health = 100f;
        }
        healthUI.fillAmount = (f / 100);

    }

    public void InputSumit(string s)
    {
        if (!audioWrite.isPlaying)
        {
            audioWrite.Play();
        }
        
        inputNumber = inputText.text;
        havePaper = true;

    }
    public void InputClose()
    {
        MazzManager.mm.canMove = true;
        inputField.SetActive(false);
        havePaper = true;
    }
    private void OnTriggerEnter(Collider other)
    {
       
        tagName = other.tag;
            //Daños de enemigos
        if (other.tag == "SlimeAttack")
        {
            
            //Deshabilito su collider para evitar multiples golpes en un ataque
            other.gameObject.SetActive(false);

            //Daño que me hace el slime.
            health -= 5;

        }
        if (other.tag == "FireBall")
        {
            //Deshabilito su collider para evitar multiples golpes en un ataque
            Destroy(other.gameObject);

            //Daño que me hace la bola de fuego.
            health -= 10;
        }
        if (other.tag == "GhostAttack")
        {
            //Deshabilito su collider para evitar multiples golpes en un ataque
            other.gameObject.SetActive(false);

            //Daño que me hace el Fantasma.
            health -= 20;
        }
        if (other.tag == "BaldomeroGuitar")
        {
            //deshabilito el collider de la guitarra
            other.GetComponent<BoxCollider>().enabled = false;

            //daño del guitarrazo
            health -= 20;
        }
        if (other.tag == "Chicken")
        {

            //daño del Pollo
            health -= 10;
        }

        //Interfaz
        if (other.tag.Contains("Key") || other.gameObject.name.Contains("palancaBuena") || other.gameObject.name.Contains("palancaMala"))
        {
            HelpUI(true);
            //helpE.SetActive(true);
            //dot.SetActive(false);
        }
        if (other.tag == "Verita" && inputText.text != "")
        {
            HelpUI(true);
            //helpE.SetActive(true);
            //dot.SetActive(false);
        }
        if (other.tag == "Table" && inputText.text == "")
        {
            HelpUI(true);
            //helpE.SetActive(true);
            //dot.SetActive(false);
        }

    }

    public void CheckPaper()
    {
        if (inputNumber == MazzManager.mm.correctNumber)
        {
            if (MazzManager.mm.doorRespawn.GetComponent<NavMeshObstacle>().enabled)
            {
                MazzManager.mm.doorRespawn.GetComponent<Animator>().SetTrigger("OpenDoor");
                MazzManager.mm.doorRespawn.GetComponent<NavMeshObstacle>().enabled = false;
            }

        }
        else
        {

            verita.GetComponent<VeritaController>().TellJoke();
        }
        HelpUI(false);
        //helpE.SetActive(false);
        //dot.SetActive(true);
        inputText.text = "";
    }
    
    private void OnTriggerExit(Collider other)
    {

        tagName = null;
        HelpUI(false);
        //helpE.SetActive(false);
        //dot.SetActive(true);
        verita.GetComponent<VeritaController>().DisablePanel();

    }
    public void HelpUI(bool helpB)
    {
        helpE.SetActive(helpB);
        dot.SetActive(!helpB);
    }
}
