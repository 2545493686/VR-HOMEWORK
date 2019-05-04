using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRInputModule : BaseInputModule
{
    public Player player;
    public new Camera camera;
    public SteamVR_Input_Sources targetSources;
    public SteamVR_Action_Boolean clickAction;
    public SteamVR_Action_Vector2 axisAction;

    public PointerEventData Data { get; private set; }

    private GameObject m_CurrentObject = null;

    protected override void Awake()
    {
        base.Awake();
        Data = new PointerEventData(eventSystem);
    }


    public override void Process()
    {


        Data.Reset();
        Data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);

        eventSystem.RaycastAll(Data, m_RaycastResultCache);
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

        m_CurrentObject = Data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(Data, m_CurrentObject);

        if (clickAction.GetStateDown(targetSources))
        {
            ProcessRelease(Data);
        }

        if (clickAction.GetStateUp(targetSources))
        {
            ProcessPress(Data);
        }
    }

    private void ProcessRelease(PointerEventData data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessPress(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
        GameObject pointerClickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        if (data.pointerPress == pointerClickHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
