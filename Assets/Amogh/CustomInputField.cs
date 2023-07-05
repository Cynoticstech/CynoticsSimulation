using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInputField : InputField
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        // Ignore the pointer down event for the input field
        // This prevents it from immediately getting focus
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        // Call the button's OnPointerClick method manually
        // This will register a click event only when the pointer is released
        if (eventData.dragging == false)
        {
            OnPointerClick(eventData);
        }
    }
}
