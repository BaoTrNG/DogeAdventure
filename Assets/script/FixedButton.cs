using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed = false;
    private bool wasPressed = false;
    public bool OnMount = false;
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
        // if (!Pressed)
        //   {
        // print("press");
        Pressed = true;
        //  }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //  print("release");
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