using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public static SlimeManager sm;
    public int size;
    void Start()
    {
        size = 3;
        sm = this;
    }

    void Update()
    {
        
    }
}
