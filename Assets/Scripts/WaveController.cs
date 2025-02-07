using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UIElements; // Necesario para usar TextMeshPro

public class WaveController : MonoBehaviour
{
    private int totalWaves = 5;
    public float waveDuration = 45f;
    public float redistributionTime = 10f;
    private int currentWave = 0;
    public UnityEvent onWaveStart;
    public UnityEvent onRedistributionStart;
    public UnityEvent onGameEnd;
    public float timer;

    public TextMeshProUGUI waveSoldier;

    public TextMeshProUGUI waveEnemy;

    public TextMeshProUGUI timeSoldier;

    public TextMeshProUGUI timeEnemy;

    public bool WaveOnCourse;

    public UnityEvent firstSeconds;

    public TextMeshProUGUI waveText;

    [Header("UI Timer")]
    public TextMeshProUGUI timerText; // Referencia al TextMeshProUGUI

    void Start()
    {
        firstSeconds?.Invoke();
        StartCoroutine(StartWithDelay(10f)); // Espera 10 segundos antes de iniciar las oleadas
    }

    public void WaveActivate() 
    {
        WaveOnCourse = true;
    }

    public void WaveDesActivate() 
    {
        WaveOnCourse = false;
    }

    private IEnumerator StartWithDelay(float delay)
    {
        Debug.Log($"⏳ Esperando {delay} segundos antes de comenzar...");
        timer = delay; // Inicializar el timer con el tiempo de espera
        UpdateTimerUI(); // Mostrar en UI

        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            UpdateTimerUI();
        }

        Debug.Log("✅ Iniciando oleadas...");
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log($"🔥 Oleada {currentWave} iniciada");
            UpdateWaveUI();
            onWaveStart?.Invoke();

            timer = waveDuration;
            UpdateTimerUI();

            while (timer > 0)
            {
                yield return new WaitForSeconds(1f);
                timer--;
                UpdateTimerUI();
            }

            Debug.Log($"⏳ Oleada {currentWave} finalizada");

            if (currentWave < totalWaves)
            {
                Debug.Log("🔄 Tiempo de redistribución de cartas iniciado");
                onRedistributionStart?.Invoke();

                timer = redistributionTime;
                UpdateTimerUI();

                while (timer > 0)
                {
                    yield return new WaitForSeconds(1f);
                    timer--;
                    UpdateTimerUI();
                }
            }
        }

        Debug.Log("🎮 Juego terminado");
        onGameEnd?.Invoke();
    }

    // Método para actualizar el UI con el tiempo restante
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"{Mathf.Ceil(timer)}"; // Muestra el tiempo en segundos sin decimales
        }
    }

      private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Oleada {currentWave}"; // Muestra el tiempo en segundos sin decimales
        }
    }

    //   public void UpdateWaveSoldierUI()
    // {
    //     if (waveSoldier != null)
    //     {
    //         waveSoldier.text = $"Oleada - {currentWave}"; // Muestra el tiempo en segundos sin decimales
    //     }
    // }
    //  public void UpdateWaveEnemyUI()
    // {
    //     if (waveEnemy != null)
    //     {
    //         waveEnemy.text = $"Oleada - {currentWave}"; // Muestra el tiempo en segundos sin decimales
    //     }
    // }
    
    //  public void UpdateTimerSoldierUI()
    // {
    //     if (timeSoldier != null)
    //     {
    //         float time = timer; // Muestra el tiempo en segundos sin decimales
    //         timeSoldier.text = $"Tiempo - {Mathf.Ceil(time)}"; // Muestra el tiempo en segundos sin decimales
    //     }
    // }

    //      public void UpdateTimerEnemyUI()
    // {
    //     if (timeSoldier != null)
    //     {
    //         float time = timer; // Muestra el tiempo en segundos sin decimales
    //         timeEnemy.text = $" Tiempo - {Mathf.Ceil(time)}"; // Muestra el tiempo en segundos sin decimales
    //     }
    // }
}
