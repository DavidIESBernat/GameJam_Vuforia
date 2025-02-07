using UnityEngine;

public class ImageTargetPosition : MonoBehaviour
{
    private float fixedY; // Almacena la posición inicial en Y
    private float fixedX; // Almacena la posición inicial en X

    void Start()
    {
        fixedY = transform.position.y; // Guarda la posición inicial en Y
        fixedX = transform.position.x; // Guarda la posición inicial en X
    }

    void Update()
    {
        // Mantiene la posición en Y fija mientras permite el movimiento en X y Z
        transform.position = new Vector3(fixedX, fixedY, transform.position.z);
    }
}