using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightTouchInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    public bool Pressed = false;
    public bool Allow = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {

        if (Allow)
        {
            print("touch");
            Pressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        print("release");
        Allow = true;

    }
}
