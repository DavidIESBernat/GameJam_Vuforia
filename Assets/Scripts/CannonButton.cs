using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannonButton : MonoBehaviour
{
    public Button button; // Botón en UI
    public Image fillImage; // Imagen principal (la que se revela con fillAmount)
    public float cooldownTime = 5f; // Duración del cooldown en segundos

    private bool isOnCooldown = false;

    void Start()
    {
        button.onClick.AddListener(UseButton);
        fillImage.fillAmount = 1f; // Inicia completamente clara
    }

    void UseButton()
    {
        if (!isOnCooldown)
        {
            Debug.Log("Botón utilizado!");
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

            // La imagen de color original se va revelando de izquierda a derecha
            fillImage.fillAmount = progress;

            yield return null;
        }

        // Cooldown terminado
        fillImage.fillAmount = 1f; // Imagen completamente visible
        button.interactable = true; // Reactiva el botón
        isOnCooldown = false;
    }
}
