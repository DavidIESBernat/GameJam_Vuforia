using UnityEngine;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isLeft; // Si es verdadero, gira a la izquierda; si es falso, a la derecha.
    private PlayerController rotateObject;

    void Start()
    {
        rotateObject = FindObjectOfType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rotateObject.SetRotation(isLeft);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rotateObject.StopRotation();
    }
}