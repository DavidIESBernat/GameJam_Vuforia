using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;

    private float timeElapsed = 0f; // Tiempo transcurrido en segundos

    void Update()
    {
        // Aumenta el tiempo transcurrido
        timeElapsed += Time.deltaTime;

        // Calculamos los minutos y segundos para mostrar en formato MM:SS
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Mostramos el tiempo en el formato MM:SS
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
