using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffController : MonoBehaviour
{
    //Este script se coloca en unos objetos vacíos situados en las puertas del Hall principal, las que han de desbloquearse para poder avanzar.
    //De manera, que si el GameObjecr del collider contiene en el nombre "Enable" o "Disable" va a deshabilitar o habilitar el gameObject que tiene en la variable "zone".
    //Con esto, deshabilito las habitaciónes al salir de los pasillos para ahorrar recursos.
    public GameObject zone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (transform.name.Contains("Enable"))
            {
                MazzManager.mm.Enable(zone);
            }
            else if (transform.name.Contains("Disable"))
            {
                MazzManager.mm.Disable(zone);
            }
            
        }
    }

}
