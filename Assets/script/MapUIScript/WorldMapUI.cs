using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapUI : MapUI //MonoBehaviour, IPointerDownHandler
{

    //[SerializeField] Camera MiniMapCam;


    public override void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!Pressed)
        {
            print("child press true");
            Pressed = true;
            /*PlayerTagOnMinimap.localScale = NewPlayerOnMiniMapScale;
            MiniMapCam.orthographicSize = NewMiniMapSize;
            RectTransform.localPosition = NewLocation;
            RectTransform.localScale = NewScale;*/
        }
        else
        {
            Pressed = false;

            print("child press false");

            /* PlayerTagOnMinimap.localScale = BasePlayerOnMiniMapScale;
             MiniMapCam.orthographicSize = BaseMiniMapSize;
             RectTransform.localPosition = BaseLocation;
             RectTransform.localScale = BaseScale;*/
        }
    }


}
