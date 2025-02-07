using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab1; // Primer tipo de enemigo
    public GameObject enemyPrefab2; // Segundo tipo de enemigo
    public Transform target; // Objetivo a seguir
    public float spawnRate = 1.5f; // Tiempo entre cada spawn
    public int maxEnemies = 10; // Número máximo de enemigos en 30s
    public float spawnDuration = 30f; // Duración total del spawneo
    public float spawnRadius = 2.0f; // Radio de aparición
    public GameObject spawnEffectPrefab; // Efecto de aparición

    public WaveController waveController;

    [Header("Probabilidades de Spawn")]
    [Range(0, 100)] public int probabilityEnemy1 = 70; // Probabilidad de aparición del enemigo 1 (70%)
    [Range(0, 100)] public int probabilityEnemy2 = 30; // Probabilidad de aparición del enemigo 2 (30%)

    private int enemiesSpawned = 0;
    private bool isSpawning = false;

    void Start()
    {
        if(waveController == null)
        {
            waveController = FindObjectOfType<WaveController>();
        }
        Debug.Log("✅ Spawner script inicializado.");
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (target == null)
        {
            Debug.LogWarning("⚠️ No hay target asignado en el Spawner.");
            return;
        }

        
        if (!isSpawning)
        {

            isSpawning = true;
            Debug.Log("🚀 Iniciado spawn...");
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
{
    // Esperar hasta que la oleada inicie
    while (!waveController.WaveOnCourse)
    {
        Debug.Log("⏳ Esperando a que la oleada comience...");
        yield return null; // Espera un frame antes de volver a comprobar
    }

    Debug.Log("🔥 Oleada en curso. Empezando a spawnear enemigos.");

    float elapsedTime = 0f;

    while (elapsedTime < spawnDuration && enemiesSpawned < maxEnemies)
    {
        SpawnEnemy();
        enemiesSpawned++;

        yield return new WaitForSeconds(spawnRate);
        elapsedTime += spawnRate;
    }

    isSpawning = false;
}


    private void SpawnEnemy()
    {
        if (enemyPrefab1 == null || enemyPrefab2 == null) return;

        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Iniciar la coroutine para instanciar el efecto y luego el enemigo
        StartCoroutine(SpawnWithDelay(spawnPosition));
    }

    private IEnumerator SpawnWithDelay(Vector3 spawnPosition)
    {
        if (spawnEffectPrefab != null)
        {
            Quaternion effectRotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject effect = Instantiate(spawnEffectPrefab, spawnPosition, effectRotation);
            Destroy(effect, 1f);
        }

        yield return new WaitForSeconds(0.3f);

        // **Decidir qué enemigo aparece basado en probabilidad**
        GameObject selectedEnemy = ChooseEnemyByProbability();

        // Instanciar enemigo en la posición aleatoria
        GameObject enemy = Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);

        // Asignarle el target (si tiene un script EnemyAI)
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.target = target;
            Debug.Log($"🎯 Se asignó el target {target.name} al enemigo {enemy.name}");
        }
        else
        {
            Debug.LogWarning($"⚠️ El prefab {selectedEnemy.name} no tiene el script EnemyAI.");
        }
    }

    private GameObject ChooseEnemyByProbability()
    {
        int randomValue = Random.Range(0, 100); // Generar número entre 0 y 99

        if (randomValue < probabilityEnemy1)
        {
            return enemyPrefab1; // Aparece el enemigo 1
        }
        else
        {
            return enemyPrefab2; // Aparece el enemigo 2
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        return new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
    }
}
