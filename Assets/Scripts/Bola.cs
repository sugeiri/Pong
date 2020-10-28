using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;using UnityEngine.SceneManagement;
public class Bola : MonoBehaviour
{
    public float velocidad = 40.0f;
    //Contadores de goles
    public int golesIzquierda = 0;
    public int golesDerecha = 0;
    public int TotalGoles = 0;
    //Cajas de texto de los contadores
    public Text contadorIzquierda;
    public Text contadorDerecha;
    public Text Msj;
    int ii = 5;
    int count = 0;
    //Audio Source
    AudioSource fuenteDeAudio;
    //Clips de audio
    public AudioClip audioGol, audioRaqueta, audioRebote, audioInicio, audioFin;


    public static bool Iniciar_Juego = false;
    public static bool JuegoFinalizado = false;
    public static string ganador = "";
    // Start is called before the first frame update
    void Start()
    {
        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();
        //Pongo los contadores a 0
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        fuenteDeAudio.clip = audioInicio;
        fuenteDeAudio.Play();
        Iniciar_Juego = false;
        Msj.text = "";


    }

    void Update()
    {
        if (fuenteDeAudio.isPlaying)
        {

            Debug.Log(fuenteDeAudio.clip.length);
            Debug.Log(fuenteDeAudio.time);
            if (fuenteDeAudio.time >= fuenteDeAudio.clip.length - 1)
            {

                if (!Iniciar_Juego)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.right * (velocidad);
                }

                Iniciar_Juego = true;
                Msj.text = "";
            }
            //Incremento la velocidad de la bola
            velocidad = 40.0f + TotalGoles;

        }
        else
        {
            if(Iniciar_Juego)
            {
                if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButton(0))
                {
                    //Cargo la escena de Juego
                    // Nombre de la scene del juego, en mi caso es SampleScene
                    SceneManager.LoadScene("Inicio");

                }
            }
        }




    }
    //Se ejecuta si choco con la raqueta
    void OnCollisionEnter2D(Collision2D micolision)
    {

        if (Iniciar_Juego)
        {
            //Si me choco con la raqueta izquierda
            if (micolision.gameObject.name == "RaquetaIzquierda")
            {
                //Valor de x
                int x = 1;
                //Valor de y
                int y = direccionY(transform.position,
                micolision.transform.position);
                //Vector de dirección
                Vector2 direccion = new Vector2(x, y);
                //Aplico la velocidad a la bola
                GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
                //Reproduzco el sonido de la raqueta
                fuenteDeAudio.clip = audioRaqueta;
                fuenteDeAudio.Play();
            }
            //Si me choco con la raqueta derecha
            else if (micolision.gameObject.name == "RaquetaDerecha")
            {
                //Valor de x
                int x = -1;
                //Valor de y
                int y = direccionY(transform.position,
                micolision.transform.position);
                //Vector de dirección
                Vector2 direccion = new Vector2(x, y);
                //Aplico la velocidad a la bola
                GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
                //Reproduzco el sonido de la raqueta
                fuenteDeAudio.clip = audioRaqueta;
                fuenteDeAudio.Play();
            }
            //Para el sonido del rebote
            if (micolision.gameObject.name == "Arriba" ||
            micolision.gameObject.name == "Abajo")
            {
                //Reproduzco el sonido del rebote
                fuenteDeAudio.clip = audioRebote;
                fuenteDeAudio.Play();
            }
        }


    }
    //Calculo la dirección de Y
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else {
            return 0;
        }
    }
    //Reinicio la posición de la bola
    public void reiniciarBola(string direccion)
    {
        TotalGoles++;
        //Posición 0 de la bola
        transform.position = Vector2.zero;
        //Vector2.zero es lo mismo que new Vector2(0,0);
        //Velocidad inicial de la bola
        velocidad = 40.0f; 
        //Velocidad y dirección
        if (direccion == "Derecha")
        {
            //Incremento goles al de la derecha
            golesDerecha++;
            //Lo escribo en el marcador
            contadorDerecha.text = golesDerecha.ToString();
            //Reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.right *velocidad;
            //Vector2.right es lo mismo que new Vector2(1,0)
        }
        else if (direccion == "Izquierda")
        {
            //Incremento goles al de la izquierda
            golesIzquierda++;
            //Lo escribo en el marcador
            contadorIzquierda.text = golesIzquierda.ToString();
            //Reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left *velocidad;
            //Vector2.right es lo mismo que new Vector2(-1,0)
        }
        //Reproduzco el sonido del rebote
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
        if (golesIzquierda == ii)
        {
            Reiniciar("Izquierda");
        }
        else
        {
            if (golesDerecha == ii)
            {
                Reiniciar("Derecha");


            }
        }
    }
    void Reiniciar(string pos)
    {
        velocidad = 0;
        fuenteDeAudio.clip = audioFin;
        fuenteDeAudio.Play();
        golesIzquierda = 0;
        golesDerecha = 0;
        TotalGoles = 0;
        Iniciar_Juego = false;
        JuegoFinalizado = true;
        Msj.text = "FIN DEL PARTIDO";
        ganador = "GANADOR: " + pos.ToUpper();
        SceneManager.LoadScene("Fin");
    }

}
