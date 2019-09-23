using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Jokes : MonoBehaviour
{

    //Este Script es el encargado de almacenar y dibujar los chistes o las pistas necesarias.

    public float textDrawSpeed;

    [HideInInspector]
    public string alert;
    public Text alertTxt;
    [HideInInspector]
    public string[] jokes;
    public Text jokesTxt;
    public GameObject panel;


    bool joking = false;
    int num;
    int count;
    string clue1, clue2, clue3;

    void Start()
    {
        
        clue1 = "Te voy a dar una pista...\n Las manos representan Nº Pares e Impares";
        clue2 = "Definitivamente, esto no es lo tuyo...\n Mira, tienes que organizar los dos números pares y luego los dos impares.";
        clue3 = "Mira, creo que ya te he hecho sufrir demasiado...\n La clave para salir es: \n4253\nComo agradecimiento, límpiame un poco, que por aquí son unos marranos";

        

        jokes = new string[] {
            "¿Por qué mandaron a una naranja a la cárcel? \n- Porque tenía la pulpa.",
            "¡Camarero! Este filete tiene muchos nervios \n- Normal, es que es la primera vez que se lo comen.",
            "¿Pero qué haces hablando con una zapatilla. \n- Aquí pone “CONVERSE”.",
            "Qué pasa si te expulsan de cuatro univerdades? \n... \n- Que estás perdiendo facultades.",
            "Caballero, ¿vino con el filete? \n- No, he venido yo solo.",
            "En la Farmacia: \n- ¿Tienen pastillas para el cansancio? \n- Están agotadas.",
            "¿Que hace un perro con un taladro? \n- Taladrando.",
            "¿Para qué va una caja al gimnasio? \n- Para hacerse caja fuerte.",
            "¿Cual es el pan que va en coche? \n- El 'pan que pita'",
            "¿Qué hace una rata con una metralleta? \nRATATATATATATATATATATA",
            "Doctor! Cada vez que me tomo un café, noto pinchazos el ojo! \n- ¿Ha intentado sacar la cucharilla de la taza?",
            "¿Qué hace un mudo bailando? \n- Una mudanza.",
            "Uno del lepe va al cine,y la chica de la taquilla le dice: \n- Señor, esta es la quinta vez que compra su entrada. \nY el lepero le contesta: \n- Es que el tío de la puerta me la rompe.",
            "¿Te cuento un chiste verde rápido? \n- Vale \n- Una lechuga en una moto",
            "¿Qué problema hay en comprarse un bumerán nuevo? \n- Que hay que tirar el viejo, y tirar el viejo, y tirar el viejo....",
            "En el restaurante: \n- Camarero, ¿el pescado viene solo? \n- No, no, se lo traeré yo.",
            "Si somos vecinos, y yo vivo abajo y tu vives arriba,\n- ¿podríamos decir que techo de menos?",
            "¿Qué le dice un mimo a otro mimo?\n- Este es mi-momento.",
            "Si me tomo un vino a las 6 de la mañana,\n- ¿Es tempranillo?",
            "¿Qué es el hermano Español perdido de Thor?\n-Thorero",
            "¿Sabéis quién es la patrona de los informáticos?.\n- Santa Tecla.",
            "¿Cuál es la diferencia entre una granada de fragmentación y un disco duro?\n- Que ambos se pueden desfragmentar",
            "¡¡¡Mamá, mamá, hay unos extraterrestres en la puerta!!!.\n- ¿Y qué te han dicho?.\n- Que son del Planeta Agostini",
            "Qué bonito está tu perro, ¿qué raza es?\n- Es un pastor alemán.\n- ¿Cómo se llama?\n- El señor.\n- ¿El señor?\n- Sí, porque El señor es mi pastor.",
            "Titular:\nIncendio en el zoológico, sospechan de las llamas.",
            "Mamá, mamá, ¿me haces un bocata de jamón?\n– ¿York?\n– Sí, túrk",
            "¿Cómo se despiden los químicos?\n- Ácido un placer",
            "Estaban dos hermanas sentadas en el baile de fin de curso. Una era fea, pero fea fea.\nSe acerca un chico a pedirles un baile y dice a la menos fea:\n- ¿Bailas?\n- ¡No!\n- ¿Y eso?\n- Eso es mi hermana y tampoco baila.",
            "A las tres de la madrugada un hombre llama a su médico por teléfono:\n- Ay, doctor, no puedo dormir. ¿Padezco de insomnio?\n- Y que se ha propuesto usted, ¿propagar la epidemia?",
            "¿Qué le dice una gallina deprimida a otra gallina deprimida?\n-Necesitamos A P O L L O",
            "Entra un borracho en una comisaría:\n- ¿Podría ver al hombre que robó ayer en mi casa?\n- ¿Y para qué lo quiere ver?\n- Para saber cómo entró sin despertar a mi mujer.",
            "Mi novia me ha dejado una nota en la mesa que ponía:\n'Me voy, no hay química'\n- No la entiendo,\n¿si le han anulado la clase para qué va?",
            "Tengo tres llamadas perdidas de mi oftalmólogo...\nEl de ver me llama.",
            "¿Sabías que los yonkis se lavan el pelo con champú de caballo?",
            "Oye, ¿sabes cómo se llaman los habitantes de Barcelona?\n- Hombre, pues todos no.",
            "¿Qué le dice la foca a su madre?\nI love you, mother foca.",
            "Hombre, Juan, cuánto tiempo. ¿Dónde vives ahora?\n- En Leganés.\n- Qué bien, donde el monstruo.",
            "Hay 10 tipos de personas: los que saben binario y los que no.",
            "¡Rápido, necesitamos sangre!\n- Yo soy 0 positivo.\n- Pues muy mal, necesitamos una mentalidad optimista.",
            "¿Cuál es el mejor portero del mundial?\n- Evidente ¡el de Para-guay!",
            "Andresito, ¿qué planeta va después de Marte? \n- Miércole, señorita.",
            "¿Por qué Bob Esponja no va al gimnasio?\n- Porque ya está cuadrado.",
            "¿Qué es un pelo en una cama?\n- El bello durmiente.",
            "¿Qué le dice una cereza a un espejo?\n- SERÉ-ESA YO?",
            "Había una vez un gato que tenia 16 vidas…  pasó un 4×4 y lo mató.",
            "¿Cómo haces que un pan hable?\n- Lo dejas en remojo un día y al otro día ya estará blando.",
            "Ayer dos tipos golpearon a mi suegra.\n- ¿Y no interviniste?\n- ¡No, ya sería mucho abuso pegarle entre 3!",
            "En un restaurante chino:\n- Solo tenemos calne de lata.\n- No importa. ¿De qué clase tienen?\n- De «latas de las que colen por los lincones».",
            "Edinson Cruso y le atropellaron",
            "¿Como se llama el Vino más amargo del Mundo?\n- Vino mi suegra.",
            "Iban dos en una moto y se cae el de en medio.",
            "Hola, soy español, ¿a qué quieres que te gane?\n- A Eurovisión\n- ¡Qué hijoputa!",
            "Buenos días, me gustaría alquilar Batman Forever.\n- No es posible, tiene que devolverla tomorrow.",
            "¿Cómo se dice en congoleño: “Podríamos cenar unas setas”?\n- Hongo propongo.",
            "¿El capitán?\n- Por babor.\n- Por babor, ¿el capitán?",
            "¿Qué se ve desde el edificio más alto de Toronto?\n- Toronto tontero.",
            "Camarero, este plátano está blando.\n- Pues dígale que se calle.",
            "Van dos soldados en una moto y no se caen porque van soldados..."


        };
    }

    void Update()
    {
        
    }
    public void DrawJoke()
    {
        if (!joking)
        {
            count++;
            jokesTxt.text = "";
            EnablePanel();
            
            joking = false;

            switch (count)
            {
                default:
                    StartCoroutine(DrawingJoke());
                    break;

                case 5:
                    StartCoroutine(DrawingClue(clue1));
                    break;

                case 10:
                    StartCoroutine(DrawingClue(clue2));
                    break;

                case 15:
                    StartCoroutine(DrawingClue(clue3));
                    break;


            }  
        }
    }
    public void EnablePanel()
    {
        joking = true;
        panel.SetActive(true);
    }
    public void DisablePanel()
    {
        joking = false;
        panel.SetActive(false);
    }
    public IEnumerator DrawingClue(string s)
    {
        alert = "Bueno anda, te doy una pista:";
        alertTxt.text = alert;
        for (int i = 0; i < s.Length; i++)
        {
            jokesTxt.text += s[i];
            yield return new WaitForSeconds(textDrawSpeed);
        }   
    }
    public IEnumerator DrawingJoke()
    {
        alert = "¡¡Incorrecto!!\nHe aquí tu castigo:";
        alertTxt.text = alert;
        num = Random.Range(0, jokes.Length);
        for (int i = 0; i < jokes[num].Length; i++)
        {
            jokesTxt.text += jokes[num][i];
            yield return new WaitForSeconds(textDrawSpeed);
        }
    }
}
