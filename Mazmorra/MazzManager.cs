using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazzManager : MonoBehaviour
{
    //Este es el Singleton de la Mazzmora, se usa como almacén de variables que requieren otros scripts, y es el encargado de controlar lo que ocurre en la mazzmora.
    //Habilita / Deshabilita zonas cuando el jugador se desplaza, inicia las animaciones y cámaras en función del progreso del jugador.

    public static MazzManager mm;

    //Bools de control de puerta
    //Cuando estén a True, las puertas correspondientes a su número estarán abiertas.
    #region


    public bool door1 = false;
    public NavMeshObstacle door1Obst;
    public Animator door1Anim;
    public GameObject zone1;

    public bool door2 = false;
    public NavMeshObstacle door2Obst;
    public Animator door2Anim;
    public GameObject zone2;


    public bool doorBoss = false;
    public NavMeshObstacle doorBossObst;
    public Animator doorBossAnim;
    public GameObject zoneBoss;
    #endregion

    [Header("Player")]
    public GameObject playerCam;
    public bool canMove;
    public bool dead;

    //Estas variables sirven para comprobar si quedan hijos en dichos objetos, son condición para desbloquear zonas.
    [Header("Enemy Containers")]
    #region
    public GameObject slimeContainer;
    public bool slimeCleared = false;

    public GameObject batContainer;
    public bool batCleared = false;

    public GameObject ghostContainer;
    public bool ghostCleared = false;
    #endregion

    //Gestión de las llaves que tiene el jugador.
    [Header("key Picks")]
    public bool keyA;
    public bool keyB;
    public bool keyC;

    //Las cámaras de las cinemáticas
    [Header("Cameras")]
    public GameObject camera1;
    public GameObject camera2;
    public GameObject cameraBoss;
    bool bCam1, bCam2, bCamB;

    //Vars re Respawn
    [Header("Respawn")]
    public GameObject doorRespawn;
    public GameObject panelRespawn;


    //Variables de control de zonas
    [HideInInspector]
    public bool enterZone1, exitZone1, enterZone2, exitZone2, enterZoneBoss, exitZoneBoss;
    [HideInInspector]
    public string correctNumber;


    




    void Start()
    {
        //Deshabilito todas las zonas por si nos hemos dejado algo en el Inspector habilitado
        //zone1.SetActive(false);
        //zone2.SetActive(false);
        //zoneBoss.SetActive(false);

        //Comenzamos con un Fade para suavizar la carga
        EnableRespawnPanel(true);

        mm = this;
        bCam1 = true;
        bCam2 = true;
        bCamB = true;

        correctNumber = "4253";
    }

    void Update()
    {



        //Este door1 es activado desde LevelController1 ubicado en la palanca buena de la primera habitación.
        if (door1)
        {
            door1Obst.enabled = !door1;
            door1Anim.SetTrigger("OpenDoor");
            ZoneLoader(zone1, door1);
            if (bCam1)
            {
                bCam1 = false;
                ShowCamera(camera1);
                Invoke("RestoreCamera", 2f);
            }

        }
        //Este if se controla más abajo cuando se hayan matado todos los Slimes de la zona 1
        if (door2)
        {
            door2Obst.enabled = !door2;
            door2Anim.SetTrigger("OpenDoor");
            ZoneLoader(zone2, door2);
            if (bCam2)
            {
                bCam2 = false;
                ShowCamera(camera2);
                Invoke("RestoreCamera", 2f);
            }

        }

        //Abajo se controla el bool de doorBoss, los bools de las Keys se habilitan desde el script del player
        if (doorBoss && keyA && keyB && keyC)
        {
            doorBossObst.enabled = !doorBoss;
            doorBossAnim.SetTrigger("OpenDoor");
            ZoneLoader(zoneBoss, doorBoss);
            if (bCamB)
            {
                bCamB = false;
                ShowCamera(cameraBoss);
                Invoke("RestoreCamera", 2f);
            }


        }
        //Comprobamos si ha matado a los enemigos
        if (slimeContainer.transform.childCount == 0)
        {
            slimeCleared = true;
        }
        if (batContainer.transform.childCount == 0)
        {
            batCleared = true;
        }
        if (ghostContainer.transform.childCount == 0)
        {
            ghostCleared = true;
        }

        //Si se han eliminado a los slimes de la Zona 1, se habilita el paso a la zona 2
        if (slimeCleared)
        {
            door2 = true;
        }
        //Si se han eliminado los murcielagos, y el fantasma, se habilita una de las dos condiciones para acceder a la zona 3
        if (batCleared && ghostCleared)
        {
            doorBoss = true;
        }
    }

    //Con este método cargamos y descargamos las habitaciónes de los pasillos que no usamos.
    public void ZoneLoader(GameObject go, bool b)
    {
        go.SetActive(b);
    }

    //Load Unload 
    public void Enable(GameObject go)
    {
        go.SetActive(true);
    }

    public void Disable(GameObject go)
    {
        go.SetActive(false);
    }

    //Método para mostrar la cámara correspondiente, impedir el movimiento del jugador y ocultar la E de indicación
    void ShowCamera(GameObject camera)
    {
        canMove = false;
        //playerCam.SetActive(false);
        playerCam.GetComponent<Camera>().enabled = false;
        playerCam.GetComponent <AudioListener>().enabled = false;

        //playerCam.transform.parent.gameObject.GetComponent<Player>().dot.SetActive(true);
        //playerCam.transform.parent.gameObject.GetComponent<Player>().helpE.SetActive(false);
        playerCam.transform.parent.gameObject.GetComponent<Player>().HelpUI(false);


        camera.SetActive(true);
    }

    //Deshacemos los cambios anteriores
    void RestoreCamera()
    {
        canMove = true;
        //playerCam.SetActive(true);
        playerCam.GetComponent<Camera>().enabled = true;
        playerCam.GetComponent<AudioListener>().enabled = true;
        camera1.SetActive(false);
        camera2.SetActive(false);
        cameraBoss.SetActive(false);
    }
    //Para cuando morimos.
    public void EnableRespawnPanel(bool onlyFade)
    {
        if (!onlyFade)
        {
            AudioManager.am.MazzSong();
        }
        canMove = false;
        panelRespawn.SetActive(true);
        panelRespawn.GetComponent<Animator>().enabled = true;
        Invoke("DisableRespawnPanel", 3f);
    }
    //Para cuando podemos volver a movernos después de muertos.
    void DisableRespawnPanel()
    {
        canMove = true;
        panelRespawn.GetComponent<Animator>().enabled = false;
        panelRespawn.SetActive(false);
    }
}
