using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;  // Jugador al que seguirá
    public Vector3 rotationOffset = new Vector3(0, 90, 0); // Ajuste de rotación

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player")?.transform; // Buscar al jugador automáticamente
            if (target == null) return; // Si no hay target, no hacer nada
        }

        // Hacer que el enemigo siempre mire al jugador
        transform.LookAt(target);

        // Aplicar un ajuste de rotación (modificar si el eje no es correcto)
        transform.rotation *= Quaternion.Euler(rotationOffset);
    }
}

