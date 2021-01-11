using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class UILongButtonHolder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onLongClickDown;
    public UnityEvent onLongClickUp;
    public void OnPointerDown(PointerEventData eventData)
    {
        onLongClickDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        onLongClickUp.Invoke();
    }
}
