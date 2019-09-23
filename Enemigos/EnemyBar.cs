using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    //Este script es utilizado por todos los enemigos normales para mostrar una barra de vida.
    //Siempre mira hacia el jugador, desde el script del enemigo, ejecuta el Método UpdateHealth, el cual recibe como parámetros
    //la vida actual y la vida Máxima, normaliza esos valores entre 0 y 1 para hacer un fill a la imagen correspondiente.
    //Además, en función de la vida que tenga, recolora la barra para dar un mejor feedback.

    Transform target;
    public Image healthBar;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.LookAt(new Vector3(target.position.x,0.75f,target.position.z));

    }
    public void UpdateHealth(float health, float maxHealth)
    {
        healthBar.fillAmount = Normalice(health, 0, maxHealth);
        if (Normalice(health, 0, maxHealth) <= 1f && Normalice(health, 0, maxHealth) > 0.7f)
        {
            healthBar.color = Color.green;
        }
        else if (Normalice(health, 0, maxHealth) <= 0.7f && Normalice(health, 0, maxHealth) > 0.4f)
        {
            healthBar.color = Color.yellow;
        }
        else if (Normalice(health, 0, maxHealth) <= 0.4f && Normalice(health, 0, maxHealth) > 0f)
        {
            healthBar.color = Color.red;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }     
    float Normalice(float value,float min, float max)
    {
	    return ((value - min) / (max - min));
        
    }


}
