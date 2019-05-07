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
        Data = new PointerEventData(eventSystem); //新建指针事件数据
    }


    public override void Process()
    {
        Data.Reset(); //复位
        Data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2); //设置指针坐标数据

        eventSystem.RaycastAll(Data, m_RaycastResultCache); //根据指针数据发射射线，获取被照射到的物体
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache); //设置指针下的当前物体为第一个被照射的物体

        m_CurrentObject = Data.pointerCurrentRaycast.gameObject; //设置当前物体

        m_RaycastResultCache.Clear(); //清除缓存

        HandlePointerExitAndEnter(Data, m_CurrentObject); //发送鼠标进出事件

        if (clickAction.GetStateDown(targetSources)) //获取按键按下
        {
            ProcessRelease(Data); 
        }

        if (clickAction.GetStateUp(targetSources)) //获取按键松开
        {
            ProcessPress(Data);
        }
    }

    private void ProcessRelease(PointerEventData data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast; //设置按下目标
        //递归当前目标下适合的子对象，发送按下的消息，并返回对象
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject); //获取能实现指针按下的子对象

        //Position of the press.
        data.pressPosition = data.position; //设置按下坐标
        //The GameObject that received the OnPointerDown.
        data.pointerPress = newPointerPress; //设置收到按下信息的对象
        //The object that the press happened on even if it can not handle the press event.
        data.rawPointerPress = m_CurrentObject; //按下发生时的对象
    }

    private void ProcessPress(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler); //发送给物体松开的消息
        GameObject pointerClickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject); //获取能实现指针按下的子对象

        if (data.pointerPress == pointerClickHandler) //如果和按下时的目标物体一样，发送按下消息
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null); //移除焦点物体

        data.pressPosition = Vector2.zero; //复位
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
