using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //Script para hacer pruebas con la Iluminación.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Light")
        {
            other.GetComponent<Light>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Light")
        {
            other.GetComponent<Light>().enabled = false;
        }
    }

}
