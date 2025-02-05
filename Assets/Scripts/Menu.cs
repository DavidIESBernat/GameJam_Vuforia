using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Menu : MonoBehaviour
{
    // Cargar la siguiente escena
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    // Salir del juego
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego..."); 
    }
}
