using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Camera MiniMapCam;
    [SerializeField] RectTransform PlayerTagOnMinimap;
    [SerializeField] GameObject WorldMapUI;
    private WorldMapUI WorldMap;
    public bool Pressed;

    private RectTransform RectTransform;

    public Vector3 NewPlayerOnMiniMapScale;
    private Vector3 BasePlayerOnMiniMapScale;

    public float NewMiniMapSize;
    public Vector3 NewLocation;
    public Vector3 NewScale;

    private float BaseMiniMapSize;
    private Vector3 BaseLocation;
    private Vector3 BaseScale;
    public virtual void Awake()
    {
        WorldMap = WorldMapUI.GetComponent<WorldMapUI>();
        WorldMapUI.SetActive(false);

        BasePlayerOnMiniMapScale = PlayerTagOnMinimap.localScale;
        RectTransform = GetComponent<RectTransform>();
        BaseLocation = RectTransform.localPosition;
        BaseScale = RectTransform.localScale;
        BaseMiniMapSize = MiniMapCam.orthographicSize;
        //MiniMapCam = GetComponent<Camera>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!Pressed)
        {
            print("father down");
            Pressed = true;
            WorldMapUI.SetActive(true);
            PlayerTagOnMinimap.localScale = NewPlayerOnMiniMapScale;
            MiniMapCam.orthographicSize = NewMiniMapSize;
            RectTransform.localPosition = NewLocation;
            RectTransform.localScale = NewScale;
        }
        else
        {
            Pressed = false;
            WorldMap.Pressed = false;
            WorldMapUI.SetActive(false);
            PlayerTagOnMinimap.localScale = BasePlayerOnMiniMapScale;
            MiniMapCam.orthographicSize = BaseMiniMapSize;
            RectTransform.localPosition = BaseLocation;
            RectTransform.localScale = BaseScale;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        /*  RectTransform.localPosition = BaseLocation;
          RectTransform.localScale = BaseScale;*/
        //Pressed = false;
    }
}
