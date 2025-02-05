using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación
    private bool rotateLeft = false;
    private bool rotateRight = false;

    void Update()
    {
        if (rotateLeft)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if (rotateRight)
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
    }

    public void SetRotation(bool left)
    {
        rotateLeft = left;
        rotateRight = !left;
    }

    public void StopRotation()
    {
        rotateLeft = false;
        rotateRight = false;
    }
}
