using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpAllPlayerNotSet : PopUpObject
{
    PointerEventData pointer ;
    private void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
    }
    private void Update()
    {
            Debug.Log(pointer.pointerCurrentRaycast.gameObject);
    }
}
