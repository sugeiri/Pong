using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reiniciar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
        if (Bola.JuegoFinalizado && Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(0); //or whatever number your scene is

    }
}
