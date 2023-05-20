using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButtonShield : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed = false;
    private bool wasPressed = false;

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
        Pressed = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        wasPressed = true;
    }

    public bool WasPressed()
    {
        if (wasPressed)
        {
            wasPressed = false;
            return true;
        }
        else
        {
            return false;
        }
    }

}