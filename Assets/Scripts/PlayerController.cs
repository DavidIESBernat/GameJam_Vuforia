using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación
    private bool rotateLeft = false;
    private bool rotateRight = false;

    [Header("Vida del Jugador")]
    public Slider BarraVida;  // Referencia a la barra de vida en UI
    public float maxHealth = 100f; 
    private float currentHealth; 
    public Button dañar;

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
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evita que baje de 0
        if (BarraVida != null)
        {
            BarraVida.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para curarse
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evita que pase el máximo
        if (BarraVida != null)
        {
            BarraVida.value = currentHealth;
        }
    }

    // Acción al morir
    void Die()
    {
        Debug.Log("Game Over");
    }
}
