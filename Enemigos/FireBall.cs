using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fly(transform.forward * speed);
    }


    public void Fly(Vector3 dir)
    {
        rb.AddForce(dir);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Bat")
        {
            Destroy(this.gameObject);
        }


    }
}
