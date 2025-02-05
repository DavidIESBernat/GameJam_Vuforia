using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannonButton : MonoBehaviour
{
    public Button button; // Botón en UI
    public Image fillImage; // Imagen que mostrará el cooldown visualmente
    public float cooldownTime = 5f; // Tiempo de cooldown en segundos

    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        button.onClick.AddListener(UseButton);
        fillImage.fillAmount = 1f; // Inicialmente lleno
    }

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            fillImage.fillAmount = cooldownTimer / cooldownTime;

            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                button.interactable = true;
                fillImage.fillAmount = 1f; // Recupera el color totalmente
            }
        }
    }

    void UseButton()
    {
        if (!isOnCooldown)
        {
            Debug.Log("Botón utilizado!");
            StartCooldown();
        }
    }

    void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
        button.interactable = false; // Desactiva el botón temporalmente
        StartCoroutine(CooldownEffect());
    }

    IEnumerator CooldownEffect()
    {
        while (cooldownTimer > 0)
        {
            float alpha = cooldownTimer / cooldownTime; // Calcula la transparencia
            fillImage.color = new Color(1f, 1f, 1f, alpha); // Cambia el color progresivamente
            yield return null;
        }
        fillImage.color = Color.white; // Vuelve a su color normal
    }
}