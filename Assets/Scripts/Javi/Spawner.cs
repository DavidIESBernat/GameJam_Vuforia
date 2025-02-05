using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> enemyPrefabs; // Lista de posibles enemigos a spawnear
    public Transform target; // Objetivo a seguir
    public float spawnRate = 1.5f; // Tiempo entre cada spawn
    public int maxEnemies = 10; // Número máximo de enemigos en 30s
    public float spawnDuration = 30f; // Duración total del spawneo
    public float spawnRadius = 2.0f; // Radio en el que aparecerán los enemigos (configurable)
    public GameObject spawnEffectPrefab; // Prefab del Particle System para efecto de spawn

    private int enemiesSpawned = 0;
    private bool isSpawning = false;

    void Start()
    {
        Debug.Log("✅ Spawner script inicializado."); // Comprobación de que el script está corriendo
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            Debug.Log("Iniciado");
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
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
    if (enemyPrefabs.Count == 0) return;

    // Generar una posición aleatoria en un radio alrededor del spawner
    Vector3 spawnPosition = GetRandomSpawnPosition();

    // Iniciar la coroutine para instanciar el efecto y luego el enemigo
    StartCoroutine(SpawnWithDelay(spawnPosition));
}

private IEnumerator SpawnWithDelay(Vector3 spawnPosition)
{
    // Instanciar efecto de partículas si hay un prefab asignado
    if (spawnEffectPrefab != null)
    {
        Quaternion effectRotation = Quaternion.Euler(0f, 0f, 0f);
        GameObject effect = Instantiate(spawnEffectPrefab, spawnPosition, effectRotation);
        Destroy(effect, 1f); // Destruir el efecto después de 1 segundo
    }

    // Esperar 0.3 segundos antes de instanciar el enemigo
    yield return new WaitForSeconds(0.3f);

    // Elegir enemigo aleatorio de la lista
    GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

    // Instanciar enemigo en la posición aleatoria
    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    // Asignarle el target (si tiene un script EnemyAI)
    EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
    if (enemyAI != null)
    {
        enemyAI.target = target;
    }
}


    private Vector3 GetRandomSpawnPosition()
    {
        // Generar un punto aleatorio dentro de un círculo en el plano XZ
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;

        // Mantener la Y original del spawner
        return new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
    }
}
