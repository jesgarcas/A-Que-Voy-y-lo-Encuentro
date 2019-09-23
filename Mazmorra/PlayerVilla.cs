using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVilla : MonoBehaviour
{
    public float rotationForce, speed;
    bool canMove = true;
    Transform playerCam;
    //Cosas de camara
    public float xMoveThreshold = 100.0f;
    public float yMoveThreshold = 100.0f;

    public float yMaxLimit;
    public float yMinLimit;

    float yRotCounter = 0.0f;
    float xRotCounter = 0.0f;
    NavMeshAgent navMeshAgent;

    Animator anim;


    void Start()
    {
      
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        
    }

    void Update()
    {
        //Si el Manager nos permite movernos
        if (canMove)
        {
            //Cosas de camara
            xRotCounter += Input.GetAxis("Mouse X") * xMoveThreshold * Time.deltaTime;
            yRotCounter += Input.GetAxis("Mouse Y") * yMoveThreshold * Time.deltaTime;
            yRotCounter = Mathf.Clamp(yRotCounter, yMinLimit, yMaxLimit);
            //xRotCounter = xRotCounter % 360;//Optional
            playerCam.localEulerAngles = new Vector3(-yRotCounter, xRotCounter, 0);


            navMeshAgent.Move(playerCam.forward * Input.GetAxis("Vertical") * Time.deltaTime*speed);
            navMeshAgent.Move(playerCam.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);



        }

        //if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))
        //{
        //    SceneManager.LoadScene(2);
        //}

    }
}
