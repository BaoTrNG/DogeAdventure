using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButtonNoUpEvent : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  print("is press jump " + Pressed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = !Pressed;

    }

}