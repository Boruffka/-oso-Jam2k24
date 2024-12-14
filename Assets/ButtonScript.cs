using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    [SerializeField] private GameObject buttonText;
    [SerializeField] private Vector3 posChange;

    public void Awake()
    {
         posChange = new Vector3(40, -10, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        buttonText.transform.position += posChange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        buttonText.transform.position -= posChange;
    }
}