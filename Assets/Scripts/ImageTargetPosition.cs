using UnityEngine;

public class ImageTargetPosition : MonoBehaviour
{
    private float fixedY; // Almacena la posición inicial en Y

    void Start()
    {
        fixedY = transform.position.y; // Guarda la posición inicial en Y
    }

    void Update()
    {
        // Mantiene la posición en Y fija mientras permite el movimiento en X y Z
        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
    }
}