using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcsLookAt : MonoBehaviour
{
    public Transform point;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(point);
    }
}
