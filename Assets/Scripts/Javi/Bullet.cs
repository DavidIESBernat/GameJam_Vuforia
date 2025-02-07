using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public float lifetime = 5f; // Tiempo antes de destruir la bala

    void Start()
    {
        Destroy(gameObject, lifetime); // Destruir la bala después de 5 segundos
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) // Para 3D
    {
        if (other.CompareTag("Enemy")) // Asegúrate de que los objetos tienen la etiqueta "Enemy"
        {
            Debug.Log($"💥 Bala impactó a {other.gameObject.name}");
            Destroy(other.gameObject); // Destruir el objeto impactado
            Destroy(gameObject); // Destruir la bala
        }
    }

    void OnTriggerEnter2D(Collider2D other) // Para 2D
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"💥 Bala impactó a {other.gameObject.name}");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
