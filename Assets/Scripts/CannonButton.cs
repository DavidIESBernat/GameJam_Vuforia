using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannonButton : MonoBehaviour
{
    public Button button; // Botón en UI
    public Image fillImage; // Imagen del cooldown
    public float cooldownTime = 5f; // Duración del cooldown en segundos
    public ExplosionController explosionController; // Referencia al script de explosión

    private bool isOnCooldown = false;

    void Start()
    {
        button.onClick.AddListener(UseButton);
        fillImage.fillAmount = 1f; // Inicia completamente visible
    }

    void UseButton()
    {
        if (!isOnCooldown)
        {
            Debug.Log("🔥 Disparo del cañón activado!");
            
            // **Llamar a la explosión**
            if (explosionController != null)
            {
                explosionController.TriggerExplosion();
            }
            else
            {
                Debug.LogError("⚠️ No se asignó un ExplosionController en el Inspector.");
            }

            // **Iniciar Cooldown**
            fillImage.fillAmount = 0f;
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        button.interactable = false; // Desactiva el botón
        float timer = 0f;

        while (timer < cooldownTime)
        {
            timer += Time.deltaTime;
            float progress = timer / cooldownTime;

            // La imagen de cooldown se va llenando progresivamente
            fillImage.fillAmount = progress;

            yield return null;
        }

        // Cooldown terminado
        fillImage.fillAmount = 1f;
        button.interactable = true; // Reactiva el botón
        isOnCooldown = false;
    }
}
