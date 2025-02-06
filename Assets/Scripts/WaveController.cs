using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    private int totalWaves = 5;
    private float waveDuration = 30f;
    private float redistributionTime = 10f;
    private int currentWave = 0;
    public UnityEvent onWaveStart;
    //public UnityEvent onWaveEnd;
    public UnityEvent onRedistributionStart;
    public UnityEvent onGameEnd;
    public float timer;

    public void StartWave()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log($"Oleada {currentWave} iniciada");
            onWaveStart?.Invoke();
            
            timer = waveDuration;
            while (timer > 0)
            {
                yield return new WaitForSeconds(1f);
                timer--;
            }
            
            Debug.Log($"Oleada {currentWave} finalizada");
            //onWaveEnd?.Invoke();
            
            if (currentWave < totalWaves)
            {
                Debug.Log("Tiempo de redistribución de cartas iniciado");
                onRedistributionStart?.Invoke();
                
                timer = redistributionTime;
                while (timer > 0)
                {
                    yield return new WaitForSeconds(1f);
                    timer--;
                }
            }
        }
        
        Debug.Log("Juego terminado");
        onGameEnd?.Invoke();
    }
}
