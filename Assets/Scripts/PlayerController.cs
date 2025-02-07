using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación
    private bool rotateLeft = false;
    private bool rotateRight = false;

    [Header("Vida del Jugador")]
    public Slider BarraVida;
    public float maxHealth = 100f;
    private float currentHealth;
    public Button dañar;

    [Header("Configuración de Efecto de Destrucción")]
    public GameObject destructionEffect;
    public Vector3 effectRotationOffset = new Vector3(0, 0, 0);

    [Header("Configuración de Game Over")]
    public GameObject gameOverPanel;

    public AudioSource destroySound;

    [Header("Protección")]
    public Collider protectionZone; // Zona de protección donde el jugador no recibe daño

    void Start()
    {
        currentHealth = maxHealth;
        if (BarraVida != null)
        {
            BarraVida.maxValue = maxHealth;
            BarraVida.value = currentHealth;
        }
    }

    void Update()
    {
        if (rotateLeft)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if (rotateRight)
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
    }

    public void SetRotation(bool left)
    {
        rotateLeft = left;
        rotateRight = !left;
    }

    public void StopRotation()
    {
        rotateLeft = false;
        rotateRight = false;
    }

    // Método para recibir daño
    public void TakeDamage(float amount, Collider enemyCollider)
{
    // Verificar si el enemigo que choca está dentro de la zona de protección
    if (protectionZone != null && protectionZone.bounds.Intersects(enemyCollider.bounds))
    {
        Debug.Log("🛡️ El enemigo está dentro de la zona protegida. No se recibe daño.");
        return;
    }

    currentHealth -= amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    if (BarraVida != null)
    {
        BarraVida.value = currentHealth;
    }

    if (currentHealth <= 0)
    {
        Die();
    }
}


    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (BarraVida != null)
        {
            BarraVida.value = currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("💀 Game Over - El jugador ha muerto");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó un panel de Game Over en el Inspector.");
        }

        Time.timeScale = 0f;
    }

   void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Enemy"))
    {
        Debug.Log($"⚠️ {other.gameObject.name} ha golpeado al jugador.");

        // Pasar el collider del enemigo a TakeDamage()
        TakeDamage(10f, other);

        if (destructionEffect != null)
        {
            Quaternion effectRotation = Quaternion.Euler(effectRotationOffset);
            GameObject effect = Instantiate(destructionEffect, other.transform.position, effectRotation);
            Destroy(effect, 1.5f);
        }

                // Reproducir sonido antes de destruir el enemigo
        if (destroySound != null && destroySound.clip != null)
        {
            destroySound.PlayOneShot(destroySound.clip);
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó un sonido de destrucción o falta el AudioClip.");
        }


        Destroy(other.gameObject);

        Debug.Log($"🔥 {other.gameObject.name} ha sido destruido.");
    }
}


}
