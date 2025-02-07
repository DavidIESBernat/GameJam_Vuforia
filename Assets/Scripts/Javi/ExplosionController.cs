using System.Collections;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject explosionEffectObject; // Objeto que se activará
    public Collider explosionCollider; // Collider de detección de enemigos
    public Transform playerTransform; // Transform del jugador

    public void TriggerExplosion()
    {
        if (playerTransform == null || explosionEffectObject == null || explosionCollider == null)
        {
            Debug.LogError("⚠️ Falta asignar referencias en ExplosionController.");
            return;
        }

        Debug.Log("💥 Explosión activada desde botón");

        // **Posicionar y activar el objeto de explosión**
        explosionEffectObject.transform.position = playerTransform.position;
        explosionEffectObject.SetActive(true);

        // Desactivar el objeto tras 1.5 segundos (si es necesario)
        StartCoroutine(DisableEffectAfterTime(1.5f));

        // Activar el collider para detectar enemigos
        StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        explosionCollider.enabled = true; // Activar collider por un breve momento
        yield return new WaitForSeconds(0.1f);
        explosionCollider.enabled = false; // Desactivar después de 0.1 segundos
    }

    private IEnumerator DisableEffectAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        explosionEffectObject.SetActive(false); // Ocultar el objeto
    }

    void OnTriggerEnter(Collider other)
    {
        if (explosionCollider.enabled && other.CompareTag("Enemy"))
        {
            Debug.Log($"🔥 {other.gameObject.name} eliminado por la explosión.");
            Destroy(other.gameObject);
        }
    }
}
