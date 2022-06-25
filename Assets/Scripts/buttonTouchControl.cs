using UnityEngine;
using UnityEngine.EventSystems;

public class buttonTouchControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   
    public bool buttonPressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
    
   
}

