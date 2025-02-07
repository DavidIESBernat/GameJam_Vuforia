using UnityEngine;

public class EnemyAI : MonoBehaviour
{
     public Transform target;  // Objetivo a seguir
    public float speed = 2f;  // Velocidad de movimiento

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning($"⚠️ {gameObject.name} no tiene un target asignado.");
        }
        else
        {
            Debug.Log($"✅ {gameObject.name} tiene como target a {target.name}");
        }
    }

    void Update()
    {
        if (target != null)
        {
            Debug.Log($"🚀 {gameObject.name} moviéndose hacia {target.position}");

            // Mover solo en X y Z, manteniendo la altura original del enemigo
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
