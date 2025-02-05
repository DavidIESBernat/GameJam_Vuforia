using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Menu : MonoBehaviour
{
    public static bool leerNormas;
    // Cargar la siguiente escena
    public void Jugar()
    {
        if(leerNormas == false) {
            SceneManager.LoadScene(1); // Mostrar normas antes de cargar el juego.
            verNormas();
        } else {
            SceneManager.LoadScene(2); // Cargar juego.
        }
    }

    public void verNormas() {
        leerNormas = true;
    }

    // Salir del juego
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego..."); 
    }
}
