using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto desde donde se dispara
    public float fireRate = 1f; // Disparo cada 1 segundo

    void Start()
    {
        // Llamar a Shoot() cada "fireRate" segundos de forma repetida
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("❌ No hay un Prefab de bala asignado en Gun.");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("❌ No hay un FirePoint asignado en Gun.");
            return;
        }

        // Instanciar la bala en el firePoint con la rotación correcta
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log($"🎯 Bala disparada en {firePoint.position}");
    }
}
